using System.Security.Claims;
using FCI.BookCave.Abstractions.Contracts.Feedbacks;
using FCI.BookCave.Abstractions.Models.Feedbacks;
using FCI.BookCave.Controllers.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCI.BookCave.Controllers.Controllers.Feedbacks
{
	[Authorize(AuthenticationSchemes = "Bearer")]
	public class FeedbackController(IFeedbackService feedbackService) : BaseApiController
	{

		[HttpPost]
		public async Task<ActionResult> SubmitFeedback(FeedbackDto feedback)
		{
			var email = User.FindFirst(ClaimTypes.Email)!.Value;
			await feedbackService.SubmitFeedback(email, feedback);
			return Ok();
		}

		// Cached(1200)]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<FeedbackCategoryDto>>> GetFeedbackCategories()
		{
			return Ok(await feedbackService.GetAllFeedbackCategories());
		}
	}
}
