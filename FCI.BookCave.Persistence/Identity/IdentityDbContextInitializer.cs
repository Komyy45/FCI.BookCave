using FCI.BookCave.Domain.Contracts.DbInitialzer;
using FCI.BookCave.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FCI.BookCave.Persistence.Identity
{
	public sealed class IdentityDbContextInitializer(IdentityDbContext dbContext, UserManager<ApplicationUser> userManager) : IDbInitialzer
	{
		public async Task InitializeAsync()
		{
			var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

			if (pendingMigrations.Any())
				await dbContext.Database.MigrateAsync();
		}

		public async Task SeedAsync()
		{
			if(!dbContext.Users.Any())
			{
				ApplicationUser applicationUser = new()
				{
					Email = "youssefelkomy500@gmail.com",
					UserName = "youssefelkomy",
					EmailConfirmed = true,
					PhoneNumber = "011571297362",
					DisplayName = "YoussefElkomy88",	
				};

				var result = await userManager.CreateAsync(applicationUser, "P@ssw0rd");
			}
		}
	}
}
