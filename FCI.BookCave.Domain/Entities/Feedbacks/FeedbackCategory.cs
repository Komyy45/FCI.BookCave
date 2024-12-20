using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Entities.Common;

namespace FCI.BookCave.Domain.Entities.Feedbacks
{
	public class FeedbackCategory : BaseEntity<int>
	{
		public string Name { get; set; } = null!;

		public string Description { get; set; } = null!;
	}
}
