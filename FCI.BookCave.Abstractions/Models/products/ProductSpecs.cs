using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCI.BookCave.Abstractions.Models.products
{
	public class ProductSpecs
	{
		public string CategoryName { get; set; } = string.Empty;

		public decimal MinPrice { get; set; } = 0.0M;

		public decimal MaxPrice { get; set; } = int.MaxValue;

		public float MinRate { get; set; } = 0.0f;

		public float MaxRate { get; set; } = 5.0f;

		public int PageIndex { get; set; } = 1;

		public int PageSize { get; set; } = 5;
	}
}
