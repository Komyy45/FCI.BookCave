using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using FCI.BookCave.Domain.Contracts.DbInitialzer;
using FCI.BookCave.Domain.Entities.Identity;
using FCI.BookCave.Domain.Entities.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

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

		public async Task SeedAsync()
		{
			bool isSeeded = false;

			if(!dbContext.Categories.Any())
			{
				 var data = await File.ReadAllTextAsync("../FCI.BookCave.Persistence/data/Seeds/Categories.json");
				 var categories = JsonSerializer.Deserialize<List<Category>>(data);
				 if(categories?.Count > 0)
				{
					await dbContext.Categories.AddRangeAsync(categories);
					isSeeded = true;
				}
			}
			if(!dbContext.Authors.Any())
			{
				 var data = await File.ReadAllTextAsync("../FCI.BookCave.Persistence/data/Seeds/Authors.json");
				 var authors = JsonSerializer.Deserialize<List<Author>>(data);
				 if(authors?.Count > 0)
				{
					await dbContext.Authors.AddRangeAsync(authors); 
					isSeeded = true;
				}
			}
			if (!dbContext.Books.Any())
			{
				var data = await File.ReadAllTextAsync("../FCI.BookCave.Persistence/data/Seeds/Books.json");
				var books = JsonSerializer.Deserialize<List<Book>>(data);
				if (books?.Count > 0)
				{
					await dbContext.Books.AddRangeAsync(books);
					isSeeded = true;
				}
			}
			
			if (isSeeded) 
				await dbContext.SaveChangesAsync();
		}
	}
}
