using FCI.BookCave.Domain.Entities.Products;
using FCI.BookCave.Persistence.Common.Configurations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCI.BookCave.Persistence.Data.Configurations
{
	[DbContext(contextType:typeof(StoreDbContext))]
	internal class AuthorConfigurations : BaseAuditableEntityConfigurations<Author, int>
	{
		public override void Configure(EntityTypeBuilder<Author> builder)
		{
			base.Configure(builder);

			builder.Property(a => a.Name)
				   .IsRequired();

			builder.Property(a => a.Bio)
				   .IsRequired();

			builder.Property(a => a.PictureUrl)
				   .IsRequired();	

			builder.Property(a => a.BirthDate)
				   .IsRequired();

			// The Relationship is configured in BookConfigurations Configuration class
		}
	}
}
