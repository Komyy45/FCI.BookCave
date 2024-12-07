using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Entities.Common;

namespace FCI.BookCave.Domain.Entities.Products
{
	public class Author : BaseAuditableEntity<int>
	{
		public string Name { get; set; } = null!;

		public string Bio { get; set; } = null!;

		public string PictureUrl { get; set; } = null!;

		public DateOnly BirthDate { get; set; }

		public virtual ICollection<Book> Books { get; set; } = new HashSet<Book>();
	}
}
