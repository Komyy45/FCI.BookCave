using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Abstractions.Models.basket;
using FCI.BookCave.Abstractions.Models.Orders;
using FCI.BookCave.Abstractions.Models.payment;

namespace FCI.BookCave.Abstractions.Contracts.Payment
{
	public interface IPaymentService
	{
		public Task<PaymentLinkDto> CreatePaymentLink(OrderDto order);

		public Task UpdateOrderStatus(string json, string signature);
	}
}
