using System.Text;
using FCI.BookCave.Abstractions.Contracts.Caching;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace FCI.BookCave.Controllers.Filters
{
	public class CachedAttribute(int timeToLiveInSeconds) : Attribute, IAsyncActionFilter
	{
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

			string key = GenerateResponseKey(context.HttpContext);

			var cachedReponse = await cacheService.GetCachedResponseAsync(key);

			if (cachedReponse is not null)
			{
				context.Result = new ContentResult()
				{
					Content = cachedReponse,
					ContentType = "application/json",
					StatusCode = 200
				};
				return;
			}
				

			var actionExecutionContext = await next();

			var response = actionExecutionContext.Result;

			if(response is OkObjectResult okObjectResult)
			{

				await cacheService.CacheResponseAsync(key, okObjectResult.Value, TimeSpan.FromSeconds(timeToLiveInSeconds));

			}
		}

		private string GenerateResponseKey(HttpContext context)
		{
			StringBuilder key = new StringBuilder(context.Request.Path);


			if(context.Request.Query.Count > 0)
				foreach(var (identifier, value) in context.Request.Query.OrderBy(x => x.Key))
				{
					key.Append($"{identifier}={value}");
				}

			return key.ToString();
		}
	}
}
