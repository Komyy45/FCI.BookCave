using FCI.BookCave.Abstractions.Contracts.Products;
using FCI.BookCave.Abstractions.Models.products;
using FCI.BookCave.Application.Mapping;
using FCI.BookCave.Domain.Contracts.UnitOfWork;
using FCI.BookCave.Domain.Entities.Products;
using FCI.BookCave.Domain.Exception;
using FCI.BookCave.Domain.Specifications.Products;

namespace FCI.BookCave.Application.Services.Products
{
	public class ProductService(IApplicationUnitOfWork unitOfWork) : IProductService
	{
		public async Task<Pagination<AuthorDto>> GetAllAuthorsAsync(int pageSize, int pageIndex)
		{
			var authorsRepository = unitOfWork.GetRepository<Author, int>();
			var authorSpecifications = new AuthorSpecifications(pageSize, pageIndex);
			var authors = await authorsRepository.GetAll();
			var pagination = new Pagination<AuthorDto>()
			{
				PageIndex = pageIndex,
				PageSize = pageSize,
				Count = await authorsRepository.CountAsync(),
				Items = MapperlyMapper.ToDto(authors)
			};

			return pagination;
		}

		public async Task<IEnumerable<CategoryDto>> GetAllCategories()
			=> MapperlyMapper.ToDto(await unitOfWork.GetRepository<Category, int>().GetAll());

		public async Task<Pagination<BookDto>> GetAllProductsAsync(ProductSpecs spec)
		{
			var booksRepository = unitOfWork.GetRepository<Book, int>();
			var authorSpecifications = new BookSpecifications(spec.PageIndex, spec.PageSize, b =>
			(b.Price <= spec.MaxPrice && b.Price >= spec.MinPrice) &&
			(b.Rate <= spec.MaxRate && b.Rate >= spec.MinRate) &&
			(spec.CategoryName == string.Empty || b.Categories.Any(c => c.Name == spec.CategoryName))
			);
			var books = await booksRepository.GetAll();
			var pagination = new Pagination<BookDto>()
			{
				PageIndex = spec.PageIndex,
				PageSize = spec.PageSize,
				Count = await booksRepository.CountAsync(),
				Items = MapperlyMapper.ToDto(books)
			};

			return pagination;
		}

		public async Task<AuthorDetailsDto> GetAuthorByIdAsync(int id)
		{
			var authorSpecifications = new AuthorSpecifications();
			var author = await unitOfWork.GetRepository<Author, int>().Get(authorSpecifications, id);

			if (author is null) throw new NotFoundException($"The Auhtor with Id: {id} doesn't exist");

			var specs = new ProductSpecs()
			{
				PageIndex = 1,
				PageSize = 5,
			};
			return MapperlyMapper.ToDto(author, await GetAllProductsAsync(specs));
		}

		public async Task<BookDetailsDto> GetBookByIdAsync(int id)
		{
			var bookSpecifications = new BookSpecifications();
			var book = await unitOfWork.GetRepository<Book, int>().Get(bookSpecifications, id);

			if (book is null) throw new NotFoundException($"The Book with Id: {id} doesn't exist");

			return MapperlyMapper.ToDto(book, book.Author.Name);
		}
	}
}
