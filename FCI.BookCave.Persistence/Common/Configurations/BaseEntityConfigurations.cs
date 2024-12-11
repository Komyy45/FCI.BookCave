using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCI.BookCave.Persistence.Common.Configurations
{
	public class BaseEntityConfigurations<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public virtual void Configure(EntityTypeBuilder<TEntity> builder)
		{
			builder.Property(b => b.Id)
				.ValueGeneratedOnAdd();
		}
	}
}
