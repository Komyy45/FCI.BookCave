using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Entities.Products;

namespace FCI.BookCave.Domain.Specifications.Products
{
	public class AuthorSpecifications : BaseSpecifications<Author, int>
	{
		public AuthorSpecifications(int pageSize, int pageIndex)
		{
			ApplyPagination(pageSize, (pageIndex - 1) * pageSize);
		}

		public AuthorSpecifications()
		{}
	}
}
