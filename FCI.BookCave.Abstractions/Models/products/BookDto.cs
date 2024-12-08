using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace FCI.BookCave.Abstractions.Models.products
{
	public class BookDto
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public float Rate { get; set; }

		public decimal Price { get; set; }

		public string PictureUrl { get; set; } = null!;
	}
}
