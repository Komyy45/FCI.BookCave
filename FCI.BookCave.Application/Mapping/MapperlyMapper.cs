using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Abstractions.Models.Identity;
using FCI.BookCave.Abstractions.Models.products;
using FCI.BookCave.Domain.Entities.Identity;
using FCI.BookCave.Domain.Entities.Products;
using Microsoft.AspNetCore.Builder.Extensions;
using Riok.Mapperly.Abstractions;

namespace FCI.BookCave.Application.Mapping
{
	[Mapper]
	public static partial class MapperlyMapper
	{		
		public static partial AuthDto ToDto(ApplicationUser entity, string token, DateTime ExpiresOn, RefreshTokenDto refreshToken);
		public static partial ApplicationUser ToEntity(RegisterDto entity);
		public static partial AuthorDetailsDto ToDto(Author author, Pagination<BookDto> books);
		public static partial IEnumerable<AuthorDto> ToDto(IEnumerable<Author> authors);
		public static partial IEnumerable<BookDto> ToDto(IEnumerable<Book> books);
		public static partial BookDetailsDto ToDto(Book book, string authorName);
		public static partial IEnumerable<CategoryDto> ToDto(IEnumerable<Category> categories);
	}
}
