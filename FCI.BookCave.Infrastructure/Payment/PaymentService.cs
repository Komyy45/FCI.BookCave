using FCI.BookCave.Abstractions.Contracts.Payment;
using FCI.BookCave.Abstractions.Models.Orders;
using FCI.BookCave.Abstractions.Models.payment;
using FCI.BookCave.Domain.Contracts.UnitOfWork;
using FCI.BookCave.Domain.Entities.Orders;
using FCI.BookCave.Domain.Enums;
using FCI.BookCave.Domain.Exception;
using FCI.BookCave.Domain.Options.Stripe;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace FCI.BookCave.Infrastructure.Payment
{
	internal class PaymentService(ProductService productService, 
		PriceService priceService, 
		PaymentLinkService paymentLinkService,
		PaymentIntentService paymentIntentService,
		ILogger<PaymentService> logger,
		IApplicationUnitOfWork unitOfWork,
		IOptions<StripeSettings> stripeSettings) : IPaymentService
	{
		private readonly string _webHookSecret = "whsec_7c7dc5461df0b05d346cb155cec68d8dd480e5bb99dd710780faea041539f10d";
		private readonly StripeSettings _stripeSettings = stripeSettings.Value;
		public async Task<PaymentLinkDto> CreatePaymentLink(OrderDto order)
		{
			var requestOptions = new RequestOptions()
			{
				ApiKey = _stripeSettings.ApiKey
			};

			var paymentLinkItemOptions = new List<PaymentLinkLineItemOptions>();
			foreach (var item in order.Items)
			{
				var product = await productService.CreateAsync(new ProductCreateOptions() { Name = item.ProductName }, requestOptions);
				var price = await priceService.CreateAsync(new PriceCreateOptions() { 
					Currency = "usd", 
					Product = product.Id, 
					UnitAmount = (long)(item.Price * 100),
				}, requestOptions);
				paymentLinkItemOptions.Add(new PaymentLinkLineItemOptions()
				{
					Price = price.Id,
					Quantity = item.Quantity,
				});
			}

			var paymentLink = await paymentLinkService.CreateAsync(new PaymentLinkCreateOptions()
			{
				LineItems = paymentLinkItemOptions,
				Metadata = new()
				{
					{ "order-id", $"{order.Id}" }
				}
			}, requestOptions);

			return new PaymentLinkDto() { Url = paymentLink.Url };
		}

		public async Task UpdateOrderStatus(string json, string signature)
		{
			try
			{
				var stripeEvent = EventUtility.ConstructEvent(json,
					 signature, _webHookSecret, throwOnApiVersionMismatch: false);
				
				if (stripeEvent.Type == "checkout.session.completed")
				{
					var session = stripeEvent.Data.Object as Session;
					var orderId = int.Parse(session!.Metadata["order-id"]);
					Order order = null!;

					var paymentIntent = await paymentIntentService.GetAsync(session.PaymentIntentId, requestOptions: new RequestOptions() { ApiKey = _stripeSettings.ApiKey});

					switch (paymentIntent.Status)
					{
						case "succeeded":
							order = await SetOrderStatus(orderId, OrderStatus.PaymentReceived);
							break;
						default:
							order = await SetOrderStatus(orderId, OrderStatus.PaymentFailed);
							break;
					}

					if (order is not null)
						logger.LogInformation($"The Order With Id: {order.Id} has Status Of {stripeEvent.Type}");
				}
			}
			catch (StripeException e)
			{
				logger.LogError(e.Message, e.StackTrace);
			}

		}

		private async Task<Order> SetOrderStatus(int orderId, OrderStatus status)
		{
			var ordersRepository = unitOfWork.GetRepository<Order, int>();

			var order = await ordersRepository.Get(orderId);

			if (order is null) throw new NotFoundException("The Order With Id {} doesn't exists!"); 

			order.Status = status;

			ordersRepository.Update(order);

			await unitOfWork.CompleteAsync();

			return order;
		}
	}
}
