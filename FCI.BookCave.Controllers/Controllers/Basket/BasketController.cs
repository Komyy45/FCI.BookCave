using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Abstractions.Contracts.basket;
using FCI.BookCave.Abstractions.Models.basket;
using FCI.BookCave.Controllers.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCI.BookCave.Controllers.Controllers.Basket
{
	[Authorize(AuthenticationSchemes = "Bearer")]
	public class BasketController(IBasketService basketService) : BaseApiController
	{
		[HttpGet]
		public async Task<ActionResult<CustomerBasketDto>> GetBasket(string id) 
			=> Ok(await basketService.GetBasketByIdAsync(id));

		[HttpPost]
		public async Task<ActionResult<CustomerBasketDto>> UpdateBasket(CustomerBasketDto basket)
			=> await basketService.UpdateBasketAsync(basket);

		[HttpDelete]
		public async Task<ActionResult<CustomerBasketDto>> DeleteBasket(string id)
			=> await basketService.DeleteBasketAsync(id);
	}
}
