using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Abstractions.Models.products;

namespace FCI.BookCave.Abstractions.Contracts.Products
{
	public interface IProductService
	{
		public Task<Pagination<BookDto>> GetAllProductsAsync(ProductSpecs spec);

		public Task<BookDetailsDto> GetBookByIdAsync(int id);

		public Task<Pagination<AuthorDto>> GetAllAuthorsAsync(int pageSize,int pageIndex);

		public Task<AuthorDetailsDto> GetAuthorByIdAsync(int id);

		public Task<IEnumerable<CategoryDto>> GetAllCategories();
	}
}
