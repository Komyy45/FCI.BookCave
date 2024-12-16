using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace FCI.BookCave.Domain.Specifications.Products
{
	public class BookSpecifications : BaseSpecifications<Book, int>
	{
		public BookSpecifications(int PageIndex, int PageSize, Expression<Func<Book,bool>> criteria) : base(criteria)
		{
			ApplyPagination(PageSize, (PageIndex - 1) * PageSize);
			AddIncludes();
		}
		public BookSpecifications()
		{
			AddIncludes();
		}

		public override void AddIncludes()
		{
			Includes.Add(b => b.Categories);
			Includes.Add(b => b.Author);
		}
	}
}
