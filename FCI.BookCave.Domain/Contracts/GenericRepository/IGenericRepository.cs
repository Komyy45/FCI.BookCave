using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Entities.Common;

namespace FCI.BookCave.Domain.Contracts.GenericRepository
{
	public interface IGenericRepository<TEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public Task<IEnumerable<TEntity>> GetAll(bool withAsNoTracking = false);
		public Task<TEntity?> Get(TKey id);
		public Task Add(TEntity entity);
		public void Update(TEntity entity);
		public void Delete(TEntity entity);
 	}
}
