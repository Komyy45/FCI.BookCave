using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Entities.Common;
using FCI.BookCave.Persistence.Common.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCI.BookCave.Persistence.Identity.Configurations
{
	public class AddressConfigurations : BaseAuditableEntityConfigurations<Address, int>
	{
		public override void Configure(EntityTypeBuilder<Address> builder)
		{
			base.Configure(builder);

			builder.Property(a => a.FirstName)
				   .HasColumnType("Varchar(30)")
					.HasMaxLength(30)
				   .IsRequired();	

			builder.Property(a => a.LastName)
				   .HasColumnType("Varchar(30)")
					.HasMaxLength(30)
				   .IsRequired();	

			builder.Property(a => a.Street)
				   .HasColumnType("Varchar(30)")
					.HasMaxLength(30)
				   .IsRequired();	

			builder.Property(a => a.City)
				   .HasColumnType("Varchar(30)")
					.HasMaxLength(30)
				   .IsRequired();	

			builder.Property(a => a.Country)
				   .HasColumnType("Varchar(30)")
					.HasMaxLength(30)
				   .IsRequired();	
		}
	}
}
