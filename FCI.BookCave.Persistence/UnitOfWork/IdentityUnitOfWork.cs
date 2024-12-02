using FCI.BookCave.Persistence.Identity;

namespace FCI.BookCave.Persistence.UnitOfWork
{
	public sealed class IdentityUnitOfWork : UnitOfWork
	{
        public IdentityUnitOfWork(IdentityDbContext context) :base(context)
        { }
    }
}
