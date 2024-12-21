using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Entities.Common;
using FCI.BookCave.Domain.Enums;

namespace FCI.BookCave.Domain.Entities.Orders
{
	public class Order : BaseEntity<int>
	{
		public string BuyerEmail { get; set; } = null!;

		public Address ShippingAddress { get; set; } = null!;

		public OrderStatus Status { get; set; } = OrderStatus.Pending;

		public DateTime Date { get; set; } = DateTime.UtcNow;

		public virtual ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

		public decimal Total { get; set; }
	}
}
