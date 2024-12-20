using System.Data;
using System.Security.Claims;
using FCI.BookCave.Abstractions.Contracts;
using FCI.BookCave.Abstractions.Models.Identity;
using FCI.BookCave.Controllers.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FCI.BookCave.Controllers.Controllers
{
	public class AccountController(IAuthService authService) : BaseApiController
	{
		[HttpPost("Login")]
		public async Task<ActionResult<AuthDto>> Login(LoginDto model)
		{
			var result = await authService.Login(model);
			SetRefreshToken(result.RefreshToken);
			return Ok(result);
		}

		[HttpPost("Register")]
		public async Task<ActionResult<AuthDto>> Register(RegisterDto model)
		{
			var result = await authService.Register(model);
			SetRefreshToken(result.RefreshToken);
			return Ok(result);
		}

		[Authorize(AuthenticationSchemes = "Bearer")]
		[HttpGet]
		public async Task<ActionResult<UserDetailsDto>> GetCurrentLoggedInUser()
		{
			return Ok(await authService.GetCurrentLoggedInUser(User.FindFirst(ClaimTypes.Email)!.Value));
		}

		[Authorize(AuthenticationSchemes = "Bearer")]
		[HttpPut]
		public async Task<ActionResult<UserDetailsDto>> UpdateCurrentUserData(UserDetailsDto userDetails)
		{
			return Ok(await authService.UpdateUserPersonalInformation(User.FindFirst(ClaimTypes.Email)!.Value ,userDetails));
		}

		private void SetRefreshToken(RefreshTokenDto refreshToken)
		{
			CookieOptions options = new CookieOptions()
			{
				HttpOnly = true,
				Expires = refreshToken.ExpiresOn
			};

			Response.Cookies.Append("refresh-token", refreshToken.Token);
		}


		[HttpPost("Refresh-Token")]
		public async Task<ActionResult<AuthDto>> RefereshToken(string token)
		{
			var result = await authService.RefreshTokenAsync(Request.Cookies["refresh-token"]);

			SetRefreshToken(result.RefreshToken);

			return Ok(result);
		}

		[HttpPost("revoke-token")] 
		public async Task<ActionResult<bool>> RevokeToken(string token)
		{
			var result = await authService.RevokeToken(token);
			return Ok(result);
		}
	}
}
