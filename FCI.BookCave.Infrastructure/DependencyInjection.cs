using FCI.BookCave.Domain.Contracts.Basket;
using FCI.BookCave.Infrastructure.Basket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace FCI.BookCave.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IConnectionMultiplexer>(sp =>
			{
				var connectionString = configuration.GetConnectionString("RedisConnection");
				return ConnectionMultiplexer.Connect(connectionString!);
			});

			services.AddScoped<IBasketRepository, BasketRepository>();

			return services;
		}
	}
}
