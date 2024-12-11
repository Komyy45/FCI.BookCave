using FCI.BookCave.Abstractions.Contracts.Products;
using FCI.BookCave.Abstractions.Models.products;
using FCI.BookCave.Controllers.Controllers.Common;
using Microsoft.AspNetCore.Mvc;

namespace FCI.BookCave.Controllers.Controllers.Products
{
	public class ProductController(IProductService productService) : BaseApiController
	{
		[HttpPost("Books")]
		public async Task<ActionResult<Pagination<BookDto>>> GetAllBooks(ProductSpecs specs)
		  => Ok(await productService.GetAllProductsAsync(specs));
		

		[HttpGet("Authors")]
		public  async Task<ActionResult<Pagination<AuthorDto>>> GetAllAuthors(int pageSize, int pageIndex)
		 => Ok(await productService.GetAllAuthorsAsync(pageSize, pageIndex));

		[HttpGet("Categories")]
		public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories()
		 => Ok(await productService.GetAllCategories());

		[HttpGet("Book/{id}")]
		public async Task<ActionResult<BookDetailsDto>> GetBookDetails(int id)
		 => await productService.GetBookByIdAsync(id);

		[HttpGet("Author/{id}")]
		public async Task<ActionResult<AuthorDetailsDto>> GetAuthorDetails(int id)
		 => await productService.GetAuthorByIdAsync(id);
	}
}
