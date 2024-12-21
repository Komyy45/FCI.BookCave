using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Abstractions.Models.Common;

namespace FCI.BookCave.Abstractions.Models.Identity
{
	public class UserDetailsDto
	{
		public string Email { get; set; } = null!;

		public string UserName { get; set; } = null!;

		public string DisplayName { get; set; } = null!;

		public string PhoneNumber { get; set; } = null!;

		public AddressDto Address { get; set; } = null!;
	}
}
