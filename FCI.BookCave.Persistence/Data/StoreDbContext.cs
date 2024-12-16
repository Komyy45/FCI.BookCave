using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Entities.Orders;
using FCI.BookCave.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FCI.BookCave.Persistence.Data
{
	public class StoreDbContext : DbContext
	{
		public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(AssemblyInformation.Assembly, configurationClass => configurationClass.GetCustomAttribute<DbContextAttribute>() != null && 
			configurationClass.GetCustomAttribute<DbContextAttribute>()!.ContextType == typeof(StoreDbContext));
		}

		public DbSet<Book> Books { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Author> Authors { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
	}
}
