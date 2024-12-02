using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Entities.Common;

namespace FCI.BookCave.Domain.Entities.Identity
{
	public class RefreshToken : BaseEntity<int>
	{
		public string Token { get; set; } = null!;
		public DateTime ExpiresOn { get; set; }
        public bool IsExpired => ExpiresOn < DateTime.UtcNow;
		public DateTime? RevokedOn { get; set; }
        public bool IsActive => RevokedOn is null && !IsExpired;
		public string UserId { get; set; } = null!;
    }
}
