using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Entities.Products;
using FCI.BookCave.Persistence.Common.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCI.BookCave.Persistence.Data.Configurations
{

	[DbContext(contextType: typeof(StoreDbContext))]
	internal class BookConfigurations : BaseAuditableEntityConfigurations<Book, int>
	{
		public override void Configure(EntityTypeBuilder<Book> builder)
		{
			base.Configure(builder);

			builder.Property(b => b.Description)
				   .HasColumnType("varchar(300)")
				   .IsRequired();

			builder.Property(b => b.Rate) 
				   .IsRequired();

			builder.Property(b => b.PictureUrl)
				   .IsRequired();

			builder.Property(b => b.Name)
				   .IsRequired();

			builder.Property(b => b.UnitsInStock)
				   .IsRequired();

			builder.HasOne(b => b.Author)
				   .WithMany(a => a.Books)
				   .HasForeignKey(a => a.AuthorId);

			builder.HasMany(b => b.Categories)
				   .WithMany(c => c.Books);
		}
	}
}
