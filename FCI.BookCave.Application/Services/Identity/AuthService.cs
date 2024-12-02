using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FCI.BookCave.Abstractions.Contracts;
using FCI.BookCave.Abstractions.Models.Common;
using FCI.BookCave.Abstractions.Models.Identity;
using FCI.BookCave.Application.Mapping;
using FCI.BookCave.Domain.Entities.Identity;
using FCI.BookCave.Domain.Exception;
using FCI.BookCave.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FCI.BookCave.Application.Services.Identity
{
	internal class AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings) : IAuthService
	{
		private JwtSettings _jwtSettings = jwtSettings.Value;

		public async Task<AuthDto> Login(LoginDto model)
		{
			var user = await userManager.FindByEmailAsync(model.Email);

			if (user is null) throw new NotFoundException("Invalid Login!");

			var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, true);

			if (result.IsLockedOut) throw new BadRequestException("This Account is Locked out");

			if (result.IsNotAllowed) throw new BadRequestException("This Account has not been Confirmed yet");

			return MapperlyMapper.ToDto(user, await GenerateJwtToken(user), DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes));
		}

		public async Task<AuthDto> Register(RegisterDto model)
		{
			var user = await userManager.FindByEmailAsync(model.Email);

			if(user is not null) throw new BadRequestException($"This Email has been registered before");

			var applicationUser = MapperlyMapper.ToEntity(model);

			var result = await userManager.CreateAsync(applicationUser, model.password);

			if (result.Errors.Any()) throw new BadRequestException(string.Join(',', result.Errors.Select(e => e.Description)));

			return MapperlyMapper.ToDto(applicationUser, await GenerateJwtToken(applicationUser), DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes));
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
