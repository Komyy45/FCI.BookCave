using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCI.BookCave.Abstractions.Models.products
{
	public class AuthorDetailsDto
	{
		public string Name { get; set; } = null!;

		public string Bio { get; set; } = null!;

		public string PictureUrl { get; set; } = null!;

		public DateOnly BirthDate { get; set; }

		public Pagination<BookDto> Books { get; set; } = null!;
	}
}
