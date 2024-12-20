using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Entities.Feedbacks;
using FCI.BookCave.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace FCI.BookCave.Persistence.Identity.Configurations.Feedbacks
{
	internal class FeedbackConfigurations : IEntityTypeConfiguration<Feedback>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Feedback> builder)
		{
			builder.Property(p => p.Id).UseIdentityColumn(1, 1);

			builder.Property(P => P.IssuedAt).HasDefaultValueSql("GetUtcDate()");

			builder.Property(P => P.FeedbackText).HasColumnType("Varchar(300)").IsRequired();

			builder.HasOne(P => P.Category)
				   .WithMany()
				   .HasForeignKey(P => P.FeedbackCategoryId);

			builder.HasOne(P => P.User)
				   .WithMany()
				   .HasForeignKey(P => P.UserId);

			builder.Property(P => P.Status)
				   .HasConversion(
					statusAsEnum => statusAsEnum.ToString(),
					statusAsString => Enum.Parse<FeedbackStatus>(statusAsString)
					);
		}
	}
}
