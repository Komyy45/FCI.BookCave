using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Abstractions.Models.Identity;

namespace FCI.BookCave.Abstractions.Contracts
{
	public interface IAuthService
	{
		Task<AuthDto> Login(LoginDto model);

		Task<AuthDto> Register(RegisterDto model); 
	}
}
