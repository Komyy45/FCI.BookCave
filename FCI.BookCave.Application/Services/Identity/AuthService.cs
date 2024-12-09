using System.ComponentModel.Design;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.RateLimiting;
using FCI.BookCave.Abstractions.Contracts;
using FCI.BookCave.Abstractions.Models.Common;
using FCI.BookCave.Abstractions.Models.Identity;
using FCI.BookCave.Application.Mapping;
using FCI.BookCave.Domain.Contracts.UnitOfWork;
using FCI.BookCave.Domain.Entities.Identity;
using FCI.BookCave.Domain.Exception;
using FCI.BookCave.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FCI.BookCave.Application.Services.Identity
{
	internal class AuthService(IIdentityUnitOfWork identityUnitOfWork, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings, MapperlyMapper _mapper) : IAuthService
	{
		private JwtSettings _jwtSettings = jwtSettings.Value;

		public async Task<AuthDto> Login(LoginDto model)
		{
			var user = await userManager.FindByEmailAsync(model.Email);

			if (user is null) throw new NotFoundException("Invalid Login!");

			var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, true);

			if (result.IsLockedOut) throw new BadRequestException("This Account is Locked out");

			if (result.IsNotAllowed) throw new BadRequestException("This Account has not been Confirmed yet");

			if(!result.Succeeded) throw new BadRequestException("Invalid login");
			
				var activeTokens = user!.Tokens.Where(t => t.IsActive);

				if (activeTokens.Any())
				{
					var repo = identityUnitOfWork.GetRepository<RefreshToken, int>();
					foreach (var token in activeTokens)
					{
						token.RevokedOn = DateTime.UtcNow;
						repo.Update(token);
					}
				}

				var refreshToken = await GenerateRefereshToken(user);

				await identityUnitOfWork.CompleteAsync();

				return _mapper.ToDto(user, await GenerateJwtToken(user), DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes), new RefreshTokenDto(refreshToken.Token, refreshToken.ExpiresOn));
		}

		public async Task<AuthDto> Register(RegisterDto model)
		{
			var user = await userManager.FindByEmailAsync(model.Email);

			if(user is not null) throw new BadRequestException($"This Email has been registered before");

			var applicationUser = _mapper.ToEntity(model);
			applicationUser.EmailConfirmed = true;

			var result = await userManager.CreateAsync(applicationUser, model.password);

			if (result.Errors.Any()) throw new BadRequestException(string.Join(',', result.Errors.Select(e => e.Description)));


			var refreshToken = await GenerateRefereshToken(applicationUser);

			return _mapper.ToDto(applicationUser, await GenerateJwtToken(applicationUser), 
				DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes), 
				new RefreshTokenDto(refreshToken.Token, refreshToken.ExpiresOn));
		}

		public async Task<bool> RevokeToken(string token)
		{
			if (token is null) throw new BadRequestException("There is no Token sent");

			var tokenRepo = identityUnitOfWork.GetRepository<RefreshToken, int>();

			var tokens = await tokenRepo.GetAll();

			var activeToken = tokens.SingleOrDefault(t => t.Token == token);

			if (activeToken is null) throw new NotFoundException("This token doesn't exist");

			activeToken.RevokedOn = DateTime.UtcNow;

			tokenRepo.Update(activeToken);

		    await identityUnitOfWork.CompleteAsync();
			
			return false;
		}

		public async Task<AuthDto> RefreshTokenAsync(string token)
		{
			if (token is null) throw new BadRequestException("There is no Token sent");

			var user = await userManager.Users.SingleOrDefaultAsync(u => u.Tokens.Any(u => u.Token == token));

			var refreshToken = user!.Tokens.FirstOrDefault(u => u.Token == token);

			if (refreshToken is null) throw new NotFoundException("There is no Such a token exists");

			if (refreshToken.IsExpired) throw new BadRequestException("The Token has been exipred");

			if (!refreshToken.IsActive) throw new BadRequestException("This token is not active right now");

			refreshToken.RevokedOn = DateTime.UtcNow;

			var generatedRefreshToken = await GenerateRefereshToken(user);

		    await identityUnitOfWork.CompleteAsync();

			return new AuthDto()
			{
				Email = user.Email!,
				UserName = user.UserName!,
				ExpiresOn = generatedRefreshToken.ExpiresOn,
				PhoneNumber = user.PhoneNumber!,
				DisplayName = user.DisplayName,
				Token = await GenerateJwtToken(user),
				RefreshToken = new RefreshTokenDto(generatedRefreshToken.Token, generatedRefreshToken.ExpiresOn)
			};
		}

		private async Task<RefreshToken> GenerateRefereshToken(ApplicationUser user)
		{
			RefreshToken token = new RefreshToken()
			{
				Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(256)),
				ExpiresOn = DateTime.UtcNow.AddDays(10),
				UserId = user.Id
			};

			await identityUnitOfWork.GetRepository<RefreshToken, int>().Add(token);

			return token;
		}

		private async Task<string> GenerateJwtToken(ApplicationUser applicationUser)
		{
			var userClaims = await userManager.GetClaimsAsync(applicationUser);
			var roles = await userManager.GetRolesAsync(applicationUser);

			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.PrimarySid, applicationUser.Id),
				new Claim(ClaimTypes.Email, applicationUser.Email!),
				new Claim(ClaimTypes.GivenName, applicationUser.DisplayName)
			}.Union(userClaims)
			 .Union(roles.Select(role => new Claim(ClaimTypes.Role, role)));

			var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
			var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

			var jsonWebToken = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,
				audience: _jwtSettings.Audience,
				expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
				claims: claims,
				signingCredentials: signingCredentials
				);

			return new JwtSecurityTokenHandler().WriteToken(jsonWebToken);
		}
	}
}
