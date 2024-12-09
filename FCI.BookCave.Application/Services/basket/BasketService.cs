using FCI.BookCave.Abstractions.Contracts.basket;
using FCI.BookCave.Abstractions.Models.basket;
using FCI.BookCave.Application.Mapping;
using FCI.BookCave.Domain.Contracts.Basket;
using FCI.BookCave.Domain.Contracts.UnitOfWork;
using FCI.BookCave.Domain.Entities.basket;
using FCI.BookCave.Domain.Entities.Products;
using FCI.BookCave.Domain.Exception;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace FCI.BookCave.Application.Services.basket
{
	internal class BasketService(IBasketRepository basketRepository, MapperlyMapper _mapper, IApplicationUnitOfWork unitOfWork) : IBasketService
	{
		public async Task<CustomerBasketDto> DeleteBasketAsync(string id)
		{
			var basket = await basketRepository.Delete(id);

			if (basket is null) throw new NotFoundException($"The basket with Id: {id} doesn't exist");

			return _mapper.ToDto(basket);
		}

		public async Task<CustomerBasketDto> GetBasketByIdAsync(string id)
		{
			var basket = await basketRepository.GetAsync(id);

			if (basket is null) throw new NotFoundException($"The basket with Id: {id} doesn't exist");

			return _mapper.ToDto(basket);
		}

		public async Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basket)
		{
			var booksRepository = unitOfWork.GetRepository<Book, int>();


			var items = new List<BasketItemDto>();
			foreach(var basketItem in basket.Items)
			{
				var item = await booksRepository.Get(basketItem.Id);
				if (item is null) throw new NotFoundException("The Book with Id: {} doesn't exist");
				basketItem.Name = item.Name;
				basketItem.Price = item.Price;
				basket.TotalPrice += (item.Price * basketItem.Quantity);
				items.Add(basketItem);
			}
			basket.Items = items;

			var customerBasket = _mapper.ToEntity(basket);

			var updatedBasket = await basketRepository.UpdateAsync(customerBasket);

			return basket;
		}
	}
}
