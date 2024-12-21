using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Entities.Feedbacks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCI.BookCave.Persistence.Identity.Configurations.Feedbacks
{
	internal class FeedbackCategoryConfigurations : IEntityTypeConfiguration<FeedbackCategory>
	{
		public void Configure(EntityTypeBuilder<FeedbackCategory> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).UseIdentityColumn(10, 10);

			builder.Property(x => x.Name).HasColumnType("Varchar(50)");

			builder.Property(x => x.Description).HasColumnType("Varchar(150)");
		}
	}
}
