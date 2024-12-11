using FCI.BookCave.Domain.Contracts.UnitOfWork;
using FCI.BookCave.Persistence.Data;

namespace FCI.BookCave.Persistence.UnitOfWork
{
	public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
	{
		public ApplicationUnitOfWork(StoreDbContext context) : base(context)
		{
		}
	}
}
