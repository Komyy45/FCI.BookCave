using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCI.BookCave.Domain.Entities.basket
{
	public class CustomerBasket
	{
		public string Id { get; set; } = null!;

		public decimal TotalPrice { get; set; }

		public IEnumerable<BasketItem> Items { get; set; } = null!;
	}
}
