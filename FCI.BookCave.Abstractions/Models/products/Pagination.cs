using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCI.BookCave.Abstractions.Models.products
{
	public class Pagination<TEntity> 
	{
		public int PageIndex { get; set; }

		public int PageSize { get; set; }

		public int Count { get; set; }

		public IEnumerable<TEntity> Items { get; set; } = null!;
	}
}
