using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Entities.Orders;
using FCI.BookCave.Domain.Enums;
using FCI.BookCave.Persistence.Common.Configurations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCI.BookCave.Persistence.Data.Configurations
{
	[DbContext(contextType: typeof(StoreDbContext))]
	public class OrderConfigurations : BaseEntityConfigurations<Order, int>
	{
		public override void Configure(EntityTypeBuilder<Order> builder)
		{
			base.Configure(builder);

			builder.Property(o => o.Status).HasConversion(
				(orderStatusAsEnum) => orderStatusAsEnum.ToString(),
				(orderStatusAsString) => Enum.Parse<OrderStatus>(orderStatusAsString)
				);

			builder.OwnsOne(o => o.ShippingAddress);
			
			builder.HasMany(o => o.Items)
				   .WithOne()
				   .HasForeignKey(o => o.OrderId);
		}
	}
}
