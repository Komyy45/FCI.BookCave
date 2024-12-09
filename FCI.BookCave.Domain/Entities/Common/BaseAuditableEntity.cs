using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FCI.BookCave.Domain.Entities.Common
{
	public class BaseAuditableEntity<TKey> : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
        public string CreatedBy { get; set; } = "1";

        public string LastModifiedBy { get; set; } = "1";

        public DateTime CreatedOn { get; set; }

        public DateTime LastModifiedOn { get; set; }
    }
}
