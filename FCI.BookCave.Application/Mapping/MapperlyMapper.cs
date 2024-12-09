using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Abstractions.Models.basket;
using FCI.BookCave.Abstractions.Models.Identity;
using FCI.BookCave.Abstractions.Models.products;
using FCI.BookCave.Domain.Entities.basket;
using FCI.BookCave.Domain.Entities.Identity;
using FCI.BookCave.Domain.Entities.Products;
using Microsoft.AspNetCore.Builder.Extensions;
using Riok.Mapperly.Abstractions;

namespace FCI.BookCave.Application.Mapping
{
	[Mapper]
	public partial class MapperlyMapper
	{
		public ImageUrlResolver _resolver { get; }

		public MapperlyMapper(ImageUrlResolver resolver)
		{
			_resolver = resolver;
		}

		public partial AuthDto ToDto(ApplicationUser entity, string token, DateTime ExpiresOn, RefreshTokenDto refreshToken);
		public partial ApplicationUser ToEntity(RegisterDto entity);
		public partial CustomerBasket ToEntity(CustomerBasketDto entity);
		public partial AuthorDetailsDto ToDto(Author author, Pagination<BookDto> books);
		public partial IEnumerable<AuthorDto> ToDto(IEnumerable<Author> authors);
		public partial IEnumerable<BookDto> ToDto(IEnumerable<Book> books);
		public partial BookDetailsDto ToDto(Book book, string authorName);
		public partial BookDto ToDto(Book book);
		public partial CustomerBasketDto ToDto(CustomerBasket book);
		public partial BookDetailsDto ToDetailsDto(Book book);
		public partial AuthorDto ToDto(Author author);
		public partial AuthorDetailsDto ToDetailsDto(Author author, Pagination<BookDto> books);
		public BookDetailsDto MapSingleDetails(Book book)
		{
			var bookDetailsDto = ToDetailsDto(book);
			bookDetailsDto.AuthorName = book.Author.Name;
			bookDetailsDto.PictureUrl = _resolver.Resolve(book.PictureUrl);
			return bookDetailsDto;
		}
		public AuthorDetailsDto MapSingleDetails(Author author, Pagination<BookDto> books)
		{
			var authorDetailsDto = ToDetailsDto(author,books);
			authorDetailsDto.PictureUrl = _resolver.Resolve(author.PictureUrl);
			return authorDetailsDto;
		}
		public AuthorDto MapSingle(Author author)
		{
			var authorDto = ToDto(author);
			authorDto.PictureUrl = _resolver.Resolve(author.PictureUrl);
			return authorDto;
		}
		public BookDto MapSingle(Book book)
		{
			var bookDto = ToDto(book);
			bookDto.PictureUrl = _resolver.Resolve(book.PictureUrl);
			return bookDto;
		}
		public IEnumerable<BookDto> MapEnumerable(IEnumerable<Book> book)
		{
			return book.Select(b => MapSingle(b));
		}
		public IEnumerable<AuthorDto> MapEnumerable(IEnumerable<Author> authors)
		{
			return authors.Select(a => MapSingle(a));
		}
		public partial IEnumerable<CategoryDto> ToDto(IEnumerable<Category> categories);
	}
}
