using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCI.BookCave.Abstractions.Models.Feedbacks
{
	public class FeedbackDto
	{
		public int UserId { get; set; }
		public string FeedbackText { get; set; } = null!;
		public int FeedbackCategoryId { get; set; }
	}
}
