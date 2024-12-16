using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Contracts.Basket;
using FCI.BookCave.Domain.Entities.basket;
using FCI.BookCave.Domain.Exception;
using StackExchange.Redis;

namespace FCI.BookCave.Infrastructure.Basket
{
	internal class BasketRepository(IConnectionMultiplexer connectionMultiplexer) : IBasketRepository
	{
		private readonly IDatabase _database = connectionMultiplexer.GetDatabase();
		public async Task<CustomerBasket?> GetAsync(string id)
		{
			var basketString = await _database.StringGetAsync(id);

			var basket = JsonSerializer.Deserialize<CustomerBasket>(basketString);

			return basket;
		}

		public async Task<CustomerBasket> UpdateAsync(CustomerBasket basket)
		{
			var basketString = JsonSerializer.Serialize(basket);

			await _database.StringSetAsync(basket.Id, basketString);

			return basket;
		}

		public async Task<CustomerBasket?> Delete(string id)
		{
			var basketString = await _database.StringGetDeleteAsync(id);

			var basket = JsonSerializer.Deserialize<CustomerBasket>(basketString);

			return basket;
		}

	}
}
