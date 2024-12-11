using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Entities.Common;
using Microsoft.AspNetCore.Http;

namespace FCI.BookCave.Domain.Entities.Products
{
    public class Book : BaseAuditableEntity<int>
    {
		public string Name { get; set; } = null!;

		public float Rate { get; set; }

		public decimal Price { get; set; }

		public string Description { get; set; } = null!;

		public int UnitsInStock { get; set; }

		public string? PictureUrl { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public virtual ICollection<Category>? Categories { get; set; } = new HashSet<Category>();

		public virtual Author? Author { get; set; }

		public int AuthorId { get; set; }
	}
}
