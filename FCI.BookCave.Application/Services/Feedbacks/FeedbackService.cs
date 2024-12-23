using FCI.BookCave.Abstractions.Contracts.Feedbacks;
using FCI.BookCave.Abstractions.Models.Feedbacks;
using FCI.BookCave.Application.Mapping;
using FCI.BookCave.Domain.Contracts.UnitOfWork;
using FCI.BookCave.Domain.Entities.Feedbacks;
using FCI.BookCave.Domain.Entities.Identity;
using FCI.BookCave.Domain.Exception;
using Microsoft.AspNetCore.Identity;

namespace FCI.BookCave.Application.Services.Feedbacks
{
	public class FeedbackService(IIdentityUnitOfWork unitOfWork, MapperlyMapper mapper, UserManager<ApplicationUser> userManager) : IFeedbackService
	{
		public async Task<IEnumerable<FeedbackCategoryDto>> GetAllFeedbackCategories()
		{
			var feedbackCategoriesRepository = unitOfWork.GetRepository<FeedbackCategory, int>();

			var categories = await feedbackCategoriesRepository.GetAll();

			return mapper.ToDto(categories);
		}

		public async Task SubmitFeedback(string email, FeedbackDto feedbackDto)
		{
			var feedbackRepository = unitOfWork.GetRepository<Feedback, int>();

			var user = await userManager.FindByEmailAsync(email);

			if (user == null) throw new NotFoundException($"The user with email:{email} doesn't exists");

			var categoriesRepository = unitOfWork.GetRepository<FeedbackCategory, int>();

			var category = await categoriesRepository.Get(feedbackDto.FeedbackCategoryId);

			if (category == null) throw new NotFoundException($"The category with id:{feedbackDto.FeedbackCategoryId} doesn't exists");

			var feedback = mapper.ToEntity(feedbackDto);

			await feedbackRepository.Add(feedback);

			await unitOfWork.CompleteAsync();
		}
	}
}
