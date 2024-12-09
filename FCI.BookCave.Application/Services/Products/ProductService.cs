using FCI.BookCave.Abstractions.Contracts.Products;
using FCI.BookCave.Abstractions.Models.products;
using FCI.BookCave.Application.Mapping;
using FCI.BookCave.Domain.Contracts.UnitOfWork;
using FCI.BookCave.Domain.Entities.Products;
using FCI.BookCave.Domain.Exception;
using FCI.BookCave.Domain.Specifications;
using FCI.BookCave.Domain.Specifications.Products;

namespace FCI.BookCave.Application.Services.Products
{
	public class ProductService(IApplicationUnitOfWork unitOfWork, MapperlyMapper _mapper) : IProductService
	{
		public async Task<Pagination<AuthorDto>> GetAllAuthorsAsync(int pageSize, int pageIndex)
		{
			var authorsRepository = unitOfWork.GetRepository<Author, int>();
			var authorSpecifications = new AuthorSpecifications(pageSize, pageIndex);
			var authors = await authorsRepository.GetAll(authorSpecifications);
			var pagination = new Pagination<AuthorDto>()
			{
				PageIndex = pageIndex,
				PageSize = pageSize,
				Count = await authorsRepository.CountAsync(),
				Items = _mapper.MapEnumerable(authors)
			};

			return pagination;
		}

		public async Task<IEnumerable<CategoryDto>> GetAllCategories()
			=> _mapper.ToDto(await unitOfWork.GetRepository<Category, int>().GetAll());

		public async Task<Pagination<BookDto>> GetAllProductsAsync(ProductSpecs spec)
		{
			var booksRepository = unitOfWork.GetRepository<Book, int>();

			var bookSpecifications = new BookSpecifications(spec.PageIndex, spec.PageSize, b =>
			(b.Price <= spec.MaxPrice && b.Price >= spec.MinPrice) &&
			(b.Rate <= spec.MaxRate && b.Rate >= spec.MinRate) &&
			(spec.CategoryId == 0 || b.Categories.Any(c => c.Id == spec.CategoryId))
			);
			var books = await booksRepository.GetAll(bookSpecifications);
			var pagination = new Pagination<BookDto>()
			{
				PageIndex = spec.PageIndex,
				PageSize = spec.PageSize,
				Count = await booksRepository.CountAsync(),
				Items = _mapper.MapEnumerable(books)
			};

			return pagination;
		}

		public async Task<AuthorDetailsDto> GetAuthorByIdAsync(int id)
		{
			var authorSpecifications = new AuthorSpecifications();
			var author = await unitOfWork.GetRepository<Author, int>().Get(authorSpecifications, id);

			if (author is null) throw new NotFoundException($"The Auhtor with Id: {id} doesn't exist");

			var pagination = new Pagination<BookDto>()
			{
				PageIndex = 1,
				PageSize = 5,
				Count = author.Books.Count,
				Items = _mapper.MapEnumerable(author.Books.Take(5))
			};
			return _mapper.MapSingleDetails(author, pagination);
		}

		public async Task<BookDetailsDto> GetBookByIdAsync(int id)
		{
			var bookSpecifications = new BookSpecifications();
			var book = await unitOfWork.GetRepository<Book, int>().Get(bookSpecifications, id);

			if (book is null) throw new NotFoundException($"The Book with Id: {id} doesn't exist");
			
			var mappedBook = _mapper.MapSingleDetails(book);
			return mappedBook!;
		}
	}
}
