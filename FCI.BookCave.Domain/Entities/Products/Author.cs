using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Entities.Common;
using Microsoft.AspNetCore.Http;

namespace FCI.BookCave.Domain.Entities.Products
{
	public class Author : BaseAuditableEntity<int>
    {
		public string Name { get; set; } = null!;

		public string Bio { get; set; } = null!;

		public string? PictureUrl { get; set; }

		public DateOnly BirthDate { get; set; }

		public virtual ICollection<Book> Books { get; set; } = new HashSet<Book>();

		[NotMapped]
		public IFormFile ImageFile { get; set; }
	}
}
