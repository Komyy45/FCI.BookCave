using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Entities.Identity;
using FCI.BookCave.Persistence.Identity;
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

			services.AddIdentityCore<ApplicationUser>(options =>
			{
				options.User.RequireUniqueEmail = true;
				options.Password.RequiredLength = 8;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = true;
				options.SignIn.RequireConfirmedEmail = true;


			}).AddEntityFrameworkStores<IdentityDbContext>();

			services.AddDbContext<IdentityDbContext>(c => c.UseSqlServer(configurations.GetConnectionString("IdentityConnection")));

			services.AddScoped<IdentityDbContextInitializer>();

			#endregion

			return services;
		}
	}
}
