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

		private void SetRefreshToken(RefreshTokenDto refreshToken)
		{
			CookieOptions options = new CookieOptions()
			{
				HttpOnly = true,
				Expires = refreshToken.ExpiresOn
			};

			Response.Cookies.Append("refresh-token", refreshToken.Token);
		}

		[Authorize(AuthenticationSchemes = "Bearer")]
		[HttpPost("Refresh-Token")]
		public async Task<ActionResult<AuthDto>> RefereshToken()
		{
			var result = await authService.RefreshTokenAsync(Request.Cookies["refresh-token"]);

			SetRefreshToken(result.RefreshToken);

			return Ok(result);
		}

		[Authorize(AuthenticationSchemes = "Bearer")]
		[HttpPost("revoke-token")] 
		public async Task<ActionResult<bool>> RevokeToken()
		{
			var result = await authService.RevokeToken(User.FindFirst(ClaimTypes.Email)!.Value, Request.Cookies["Refresh-token"]);
			return Ok(result);
		}
	}
}
