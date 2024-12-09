using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Entities.basket;

namespace FCI.BookCave.Domain.Contracts.Basket
{
	public interface IBasketRepository
	{
		public Task<CustomerBasket?> GetAsync(string id);

		public Task<CustomerBasket> UpdateAsync(CustomerBasket basket);

		public Task<CustomerBasket?> Delete(string id);
	}
}
