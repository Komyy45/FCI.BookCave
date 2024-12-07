using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FCI.BookCave.Persistence.Common.Configurations
{
	public class BaseAuditableEntityConfigurations<TEntity, TKey> : BaseEntityConfigurations<TEntity, TKey>
		where TEntity : BaseAuditableEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public override void Configure(EntityTypeBuilder<TEntity> builder)
		{	
			builder.Property(ba => ba.CreatedBy)
				   .IsRequired();
			builder.Property(ba => ba.CreatedOn)
				.HasDefaultValueSql("GetUtcDate()")
				   .IsRequired();
			builder.Property(ba => ba.LastModifiedBy)
				   .IsRequired();
			builder.Property(ba => ba.LastModifiedOn)
				   .HasComputedColumnSql("GetUtcDate()")
				   .IsRequired();
		}
	}
}
