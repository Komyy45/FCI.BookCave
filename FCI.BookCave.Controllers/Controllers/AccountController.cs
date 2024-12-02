using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Abstractions.Contracts;
using FCI.BookCave.Abstractions.Models.Identity;
using FCI.BookCave.Controllers.Controllers.Common;
using Microsoft.AspNetCore.Mvc;

namespace FCI.BookCave.Controllers.Controllers
{
	public class AccountController(IAuthService authService) : BaseApiController
	{
		[HttpPost("Login")]
		public async Task<ActionResult<AuthDto>> Login(LoginDto model)
		{
			var result = await authService.Login(model);
			return Ok(result);
		}

		[HttpPost("Register")]
		public async Task<ActionResult<AuthDto>> Register(RegisterDto model)
		{
			var result = await authService.Register(model);
			return Ok(result);
		}
	}
}
