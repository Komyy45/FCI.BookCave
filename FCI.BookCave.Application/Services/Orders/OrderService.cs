using FCI.BookCave.Abstractions.Contracts.Orders;
using FCI.BookCave.Abstractions.Contracts.Payment;
using FCI.BookCave.Abstractions.Models.Common;
using FCI.BookCave.Abstractions.Models.Orders;
using FCI.BookCave.Abstractions.Models.payment;
using FCI.BookCave.Application.Mapping;
using FCI.BookCave.Domain.Contracts.Basket;
using FCI.BookCave.Domain.Contracts.UnitOfWork;
using FCI.BookCave.Domain.Entities.Identity;
using FCI.BookCave.Domain.Entities.Orders;
using FCI.BookCave.Domain.Entities.Products;
using FCI.BookCave.Domain.Exception;
using FCI.BookCave.Domain.Specifications.Orders;
using Microsoft.AspNetCore.Identity;

namespace FCI.BookCave.Application.Services.Orders
{
	internal class OrderService(IBasketRepository basketRepository,
		UserManager<ApplicationUser> userManager, 
		IApplicationUnitOfWork unitOfWork, 
		MapperlyMapper mapper,
		IPaymentService paymentService) : IOrderService
	{
		public async Task<PaymentLinkDto> CreateOrderAsync(string buyerEmail, string basketId)
		{
			var basket = await basketRepository.GetAsync(basketId);

			if (basket is null) throw  new NotFoundException($"The basket with Id: {basketId} doesn't exists!");

		    var user = await userManager.FindByEmailAsync(buyerEmail);

			if (user is null) throw new NotFoundException($"The User with email: {buyerEmail} doesn't exists!");

			List<OrderItem> items = new();
			var booksRepository = unitOfWork.GetRepository<Book, int>();
			decimal totalPrice = 0.0m;
			foreach(var item in basket!.Items)
			{
				var product = await booksRepository.Get(item.Id);
				if (product is null) throw new NotFoundException($"The book with Id: {item.Id} doesn't exists!");
				items.Add(new OrderItem
				{
					ProductId = product.Id,
					ProductName = product.Name,
					PictureUrl = product.PictureUrl,
					Price = product.Price,
					Quantity = item.Quantity,
				});
				totalPrice += (product.Price * item.Quantity);
			}

			var order = new Order()
			{
				BuyerEmail = buyerEmail,
				ShippingAddress = user.Address,
				Items = items,
				Total = totalPrice
			};

			var ordersRepository = unitOfWork.GetRepository<Order, int>();

			await ordersRepository.Add(order);

		 	await unitOfWork.CompleteAsync();

			return await paymentService.CreatePaymentLink(mapper.MapSingle(order));
		}

		public async Task<OrderDto> GetOrderByIdAsync(int id)
		{
			var orderRepository = unitOfWork.GetRepository<Order, int>();

			OrderSpecifications specs = new OrderSpecifications();
			var order = await orderRepository.Get(specs, id);

			if (order is null) throw new NotFoundException($"The order with Id: {id} doesn't exists!");

			return mapper.MapSingle(order);
		}

		public async Task<IEnumerable<OrderDto>> GetOrdersForUserAsync(string email)
		{
			var ordersRepository = unitOfWork.GetRepository<Order, int>();

			OrderSpecifications specs = new OrderSpecifications(email);

			var userOrders = await ordersRepository.GetAll(specs);

			return mapper.MapEnumerable(userOrders);
		}

		public async Task<AddressDto> GetShippingAddress(string email)
		{
			var user = await  userManager.FindByEmailAsync(email);

			if (user is null) throw new NotFoundException($"The User with email: {email} doesn't exists!");

			return mapper.ToDto(user.Address);
		}

		public async Task<AddressDto> UpdateShippingAddress(string email, AddressDto address)
		{
		 	 var user = await userManager.FindByEmailAsync(email);

			if (user is null) throw new NotFoundException($"The User with email: {email} doesn't exists!");

			user.Address = mapper.ToEntity(address);

		 	await userManager.UpdateAsync(user);

			return address;
		}
	}
}
