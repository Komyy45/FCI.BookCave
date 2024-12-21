using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Abstractions.Models.Feedbacks;

namespace FCI.BookCave.Abstractions.Contracts.Feedbacks
{
	public interface IFeedbackService
	{
		public Task<IEnumerable<FeedbackCategoryDto>> GetAllFeedbackCategories();

		public Task SubmitFeedback(string email, FeedbackDto feedbackDto);
	}
}
