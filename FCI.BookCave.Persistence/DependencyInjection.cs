using FCI.BookCave.Domain.Contracts.UnitOfWork;
using FCI.BookCave.Persistence.Identity;
using FCI.BookCave.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FCI.BookCave.Persistence
{
	public static class DependencyInjection 
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configurations)
		{
			// Register Identity Db Services
			#region IdentityDbContext

			services.AddDbContext<IdentityDbContext>(c => 
			c.UseSqlServer(configurations.GetConnectionString("IdentityConnection"))
			.UseLazyLoadingProxies());

			services.AddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>();

			services.AddScoped<IdentityDbContextInitializer>();

			#endregion

			return services;
		}
	}
}
