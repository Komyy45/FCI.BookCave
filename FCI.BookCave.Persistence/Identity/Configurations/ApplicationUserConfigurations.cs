using FCI.BookCave.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCI.BookCave.Persistence.Identity.Configurations
{
	[DbContext(contextType : typeof(IdentityDbContext))]
	public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.OwnsOne(u => u.Address);

			builder.HasMany(u => u.Tokens)
				   .WithOne()
				   .HasForeignKey(u => u.UserId);

			builder.Property(u => u.DisplayName)
				   .HasMaxLength(50);
		}
	}
}
