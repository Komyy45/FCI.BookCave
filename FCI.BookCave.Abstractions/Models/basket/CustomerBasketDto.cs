using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCI.BookCave.Abstractions.Models.basket
{
	public class CustomerBasketDto
	{
		public string Id { get; set; } = null!;

		public decimal TotalPrice { get; set; }

		public IEnumerable<BasketItemDto> Items { get; set; } = null!;
	}
}
