using System.Security.Claims;
using FCI.BookCave.Abstractions.Contracts.Orders;
using FCI.BookCave.Abstractions.Models.Common;
using FCI.BookCave.Abstractions.Models.Orders;
using FCI.BookCave.Controllers.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCI.BookCave.Controllers.Controllers.Orders
{
	[Authorize(AuthenticationSchemes = "Bearer")]
	public class OrdersController(IOrderService orderService) : BaseApiController
	{
		[HttpGet] // GET: /api/Orders
		public async Task<ActionResult<IEnumerable<OrderDto>>> GetUserOrders()
		{
			var userEmail = User.FindFirst(ClaimTypes.Email)!.Value;

			return Ok(await orderService.GetOrdersForUserAsync(userEmail));
		}

		[HttpPost] // POST: /api/Orders?basketId=XXX
		public async Task<ActionResult<OrderDto>> CreateOrder([FromQuery] string basketId)
		{
			var userEmail = User.FindFirst(ClaimTypes.Email)!.Value;

			return Ok(await orderService.CreateOrderAsync(userEmail, basketId));
		}

		[HttpGet("{id:int}")] // GET: /api/Orders/OrderId 
		public async Task<ActionResult<OrderDto>> CreateOrder(int id)
		{
			return Ok(await orderService.GetOrderByIdAsync(id));
		}


		[HttpGet("ShippingAddress")] // GET: /api/Orders/ShippingAddress 
		public async Task<ActionResult<AddressDto>> GetUserAddress()
		{
			var userEmail = User.FindFirst(ClaimTypes.Email)!.Value;

			return Ok(await orderService.GetShippingAddress(userEmail) ?? new AddressDto());
		}

		[HttpPost("ShippingAddress")] // POST: /api/Orders/ShippingAddress
		public async Task<ActionResult<AddressDto>> UpdateShippingAddress(AddressDto address)
		{
			var userEmail = User.FindFirst(ClaimTypes.Email)!.Value;

			return Ok(await orderService.UpdateShippingAddress(userEmail, address) );
		}
	}
}
