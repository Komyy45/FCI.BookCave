using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Entities.Common;

namespace FCI.BookCave.Domain.Entities.Orders
{
	public class OrderItem : BaseEntity<int>
	{
		public int ProductId { get; set; }

		public string ProductName { get; set; } = null!;

		public string PictureUrl { get; set; } = null!;

		public decimal Price { get; set; }

		public int Quantity { get; set; }

		public int OrderId { get; set; }
	}
}
