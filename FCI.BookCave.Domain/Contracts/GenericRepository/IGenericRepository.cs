using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Contracts.Specification;
using FCI.BookCave.Domain.Entities.Common;

namespace FCI.BookCave.Domain.Contracts.GenericRepository
{
	public interface IGenericRepository<TEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public Task<IEnumerable<TEntity>> GetAll(bool withAsNoTracking = true);
		public Task<IEnumerable<TEntity>> GetAll(ISpecifications<TEntity,TKey> specs, bool withAsNoTracking = true);
		public Task<TEntity?> Get(TKey id);
		public Task<TEntity?> Get(ISpecifications<TEntity, TKey> specs, TKey id);
		public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null!);
		public Task Add(TEntity entity);
		public void Update(TEntity entity);
		public void Delete(TEntity entity);
 	}
}
