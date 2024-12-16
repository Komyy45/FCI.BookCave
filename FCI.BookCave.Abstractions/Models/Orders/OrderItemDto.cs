using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCI.BookCave.Abstractions.Models.Orders
{
	public class OrderItemDto
	{
		public int ProductId { get; set; }

		public string ProductName { get; set; } = null!;

		public string PictureUrl { get; set; } = null!;

		public decimal Price { get; set; }

		public int Quantity { get; set; }

		public int OrderId { get; set; }
	}
}
