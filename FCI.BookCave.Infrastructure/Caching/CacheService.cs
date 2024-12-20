using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FCI.BookCave.Abstractions.Contracts.Caching;
using StackExchange.Redis;

namespace FCI.BookCave.Infrastructure.Caching
{
	public class CacheService(IConnectionMultiplexer connectionMultiplexer) : ICacheService
	{
		private readonly IDatabase _database = connectionMultiplexer.GetDatabase();

		public async Task CacheResponseAsync(string key, object response, TimeSpan timeToLive) {

			if (key is null || response is null) return;

			var serailizedResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
		
			await _database.StringSetAsync(key, serailizedResponse, timeToLive);	
		}

		public async Task<string?> GetCachedResponseAsync(string key)
		{
			if (key is null) return null!;

			return await _database.StringGetAsync(key);
		}
	}
}
