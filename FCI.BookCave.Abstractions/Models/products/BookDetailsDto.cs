using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCI.BookCave.Abstractions.Models.products
{
	public class BookDetailsDto
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public float Rate { get; set; }

		public decimal Price { get; set; }

		public string Description { get; set; } = null!;

		public int UnitsInStock { get; set; }

		public string PictureUrl { get; set; } = null!;

		public IEnumerable<CategoryDto> Categories { get; set; } = null!;

		public string AuthorName { get; set; } = null!;

		public int AuthorId { get; set; }
	}
}
