using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCI.BookCave.Abstractions.Models.Identity
{
	public record AuthDto
	{
		public string UserName { get; init; }
		public string DisplayName { get; init; }
		public string Email { get; init; }
		public string PhoneNumber { get; init; }
		public string Token { get; init; }
		public DateTime ExpiresOn { get; init; }
	}
}
