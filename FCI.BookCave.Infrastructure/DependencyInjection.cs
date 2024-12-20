using FCI.BookCave.Abstractions.Contracts.Caching;
using FCI.BookCave.Abstractions.Contracts.Payment;
using FCI.BookCave.Domain.Contracts.Basket;
using FCI.BookCave.Domain.Options.Stripe;
using FCI.BookCave.Infrastructure.Basket;
using FCI.BookCave.Infrastructure.Caching;
using FCI.BookCave.Infrastructure.Payment;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Stripe;

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


			services.Configure<StripeSettings>(c =>
			{
				c.ApiKey = configuration["StripeSettings:ApiKey"]!;
			});

			services.AddSingleton(provider =>
			{
				var stripeSettings = provider.GetRequiredService<IOptions<StripeSettings>>().Value;
				StripeConfiguration.ApiKey = stripeSettings.ApiKey;
				return new StripeClient(stripeSettings.ApiKey);
			});

			services.AddScoped<ProductService>();
			services.AddScoped<PriceService>();
			services.AddScoped<PaymentLinkService>();
			services.AddScoped<PaymentIntentService>();


			services.AddScoped<IPaymentService, PaymentService>();


			services.AddScoped<ICacheService, CacheService>();

			return services;
		}
	}
}
