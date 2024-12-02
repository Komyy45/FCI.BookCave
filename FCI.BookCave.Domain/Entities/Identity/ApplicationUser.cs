using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;

namespace FCI.BookCave.Domain.Entities.Identity
{
	public class ApplicationUser : IdentityUser
	{
        public string DisplayName { get; set; } = null!;
        public ICollection<Address> Addresses { get; set; } = new HashSet<Address>();
        public ICollection<RefreshToken> Tokens { get; set; } = new HashSet<RefreshToken>();
    }
}
