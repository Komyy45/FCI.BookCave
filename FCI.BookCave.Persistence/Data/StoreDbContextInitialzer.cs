using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Contracts.DbInitialzer;
using FCI.BookCave.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FCI.BookCave.Persistence.Data
{
	public class StoreDbContextInitialzer(StoreDbContext dbContext) : IDbInitialzer
	{
		public async Task InitializeAsync()
		{
			var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

			if (pendingMigrations.Any())
				await dbContext.Database.MigrateAsync();
		}

		public Task SeedAsync()
		{
			return Task.CompletedTask;
		}
	}
}
