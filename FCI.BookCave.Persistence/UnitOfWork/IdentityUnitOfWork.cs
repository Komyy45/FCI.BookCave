using FCI.BookCave.Domain.Contracts.UnitOfWork;
using FCI.BookCave.Persistence.Identity;

namespace FCI.BookCave.Persistence.UnitOfWork
{
	public sealed class IdentityUnitOfWork : UnitOfWork, IIdentityUnitOfWork
	{
        public IdentityUnitOfWork(IdentityDbContext context) :base(context)
        { }
    }
}
