using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Abstractions.Contracts.Payment;
using FCI.BookCave.Controllers.Controllers.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FCI.BookCave.Controllers.Controllers.Payment
{
	public class PaymentController(IPaymentService paymentService) : BaseApiController
	{
		[HttpPost("webhook")]
		public async Task<IActionResult> Webhook()
		{
			var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
			var signature = Request.Headers["Stripe-Signature"];

			await paymentService.UpdateOrderStatus(json, signature);

			return Ok();
		}
	}
}
