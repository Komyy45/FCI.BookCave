using FCI.BookCave.Domain.Entities.Products;
using FCI.BookCave.Persistence.Common.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCI.BookCave.Persistence.Data.Configurations
{
	internal class CategoryConfigurations : BaseEntityConfigurations<Category, int>
	{
		public override void Configure(EntityTypeBuilder<Category> builder)
		{
			base.Configure(builder);

			builder.Property(c => c.Name)
				   .IsRequired();

			// The Relationship is configured in BookConfigurations Configuration class
		}
	}
}
