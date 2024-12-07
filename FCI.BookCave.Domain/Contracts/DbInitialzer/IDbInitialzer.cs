using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCI.BookCave.Domain.Contracts.DbInitialzer
{
	public interface IDbInitialzer
	{
		public Task InitializeAsync();

		public Task SeedAsync();
	}
}
