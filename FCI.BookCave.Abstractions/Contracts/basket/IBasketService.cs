using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Abstractions.Models.basket;
using Microsoft.VisualBasic.FileIO;

namespace FCI.BookCave.Abstractions.Contracts.basket
{
	public interface IBasketService
	{
		public Task<CustomerBasketDto> GetBasketByIdAsync(string id);

		public Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basket);

		public Task<CustomerBasketDto> DeleteBasketAsync(string id);
	}
}
