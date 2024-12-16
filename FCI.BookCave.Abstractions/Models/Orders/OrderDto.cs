using FCI.BookCave.Abstractions.Models.Common;

namespace FCI.BookCave.Abstractions.Models.Orders
{
	public class OrderDto
	{
		public int Id { get; set; }

		public string BuyerEmail { get; set; } = null!;

		public AddressDto ShippingAddress { get; set; } = null!;

		public string Status { get; set; } = null!;

		public DateTime Date { get; set; } = DateTime.UtcNow;

		public IEnumerable<OrderItemDto> Items { get; set; } = null!;

		public decimal Total { get; set; }
	}
}
