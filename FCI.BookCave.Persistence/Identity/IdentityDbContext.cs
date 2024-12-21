using System.Reflection;
using FCI.BookCave.Domain.Entities.Common;
using FCI.BookCave.Domain.Entities.Feedbacks;
using FCI.BookCave.Domain.Entities.Identity;
using FCI.BookCave.Persistence.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FCI.BookCave.Persistence.Identity
{
	public class IdentityDbContext : IdentityDbContext<ApplicationUser>
	{
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.ApplyConfigurationsFromAssembly(AssemblyInformation.Assembly, 
				configurationClass => configurationClass.GetCustomAttribute<DbContextAttribute>() is null || 
				(configurationClass.GetCustomAttribute<DbContextAttribute>()!.ContextType) == typeof(IdentityDbContext));
		}

        public DbSet<RefreshToken> RefreshTokens { get; set; }

		public DbSet<Feedback> Feedbacks { get; set; }

		public DbSet<FeedbackCategory> FeedbackCategories { get; set; }
	}
}
