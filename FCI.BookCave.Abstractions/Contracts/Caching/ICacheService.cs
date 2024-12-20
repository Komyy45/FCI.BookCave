using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCI.BookCave.Abstractions.Contracts.Caching
{
	public interface ICacheService
	{
		public Task CacheResponseAsync(string key, object response, TimeSpan timeToLive);

		public Task<string?> GetCachedResponseAsync(string key);
	}
}
