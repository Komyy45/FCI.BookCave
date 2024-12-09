using FCI.BookCave.Domain.Entities.Products;
using FCI.BookCave.Persistence.Common.Configurations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCI.BookCave.Persistence.Data.Configurations
{

	[DbContext(contextType: typeof(StoreDbContext))]
	internal class CategoryConfigurations : BaseEntityConfigurations<Category, int>
	{
		public override void Configure(EntityTypeBuilder<Category> builder)
		{
			base.Configure(builder);

			builder.Property(c => c.Name)
				   .IsRequired();
		}
	}
}
