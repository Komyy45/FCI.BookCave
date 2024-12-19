﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Abstractions.Models.Common;
using FCI.BookCave.Abstractions.Models.Orders;
using FCI.BookCave.Abstractions.Models.payment;

namespace FCI.BookCave.Abstractions.Contracts.Orders
{
	public interface IOrderService
	{
		Task<PaymentLinkDto> CreateOrderAsync(string buyerEmail, string basketId);

		Task<IEnumerable<OrderDto>> GetOrdersForUserAsync(string email);

		Task<OrderDto> GetOrderByIdAsync(int id);

		Task<AddressDto> GetShippingAddress(string email);

		Task<AddressDto> UpdateShippingAddress(string email, AddressDto address);
 	}
}
