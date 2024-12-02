using FCI.BookCave.Domain.Contracts.GenericRepository;
using FCI.BookCave.Domain.Entities.Common;

namespace FCI.BookCave.Domain.Contracts.UnitOfWork
{
	public interface IUnitOfWork : IAsyncDisposable
	{
		IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() 
			where TEntity : BaseEntity<TKey>
			where TKey :  IEquatable<TKey>;

		Task<int> CompleteAsync();
	}
}
