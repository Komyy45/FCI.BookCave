using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Entities.Common;
using FCI.BookCave.Domain.Entities.Identity;
using FCI.BookCave.Domain.Enums;

namespace FCI.BookCave.Domain.Entities.Feedbacks
{
	public class Feedback : BaseEntity<int>
	{
		public string UserId { get; set; } = null!;
		public virtual ApplicationUser User { get; set; } = null!;
		public string FeedbackText { get; set; } = null!;
		public virtual FeedbackCategory Category { get; set; } = null!;
		public int FeedbackCategoryId { get; set; }
		public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
		public FeedbackStatus Status { get; set; } = FeedbackStatus.Pending;
	}
}
