using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Contracts.GenericRepository;
using FCI.BookCave.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace FCI.BookCave.Persistence.Repositories
{
	public class GenericRepository<TEntity, TKey>(DbContext context) : IGenericRepository<TEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public async Task<IEnumerable<TEntity>> GetAll(bool withAsNoTracking = false)
		{
			var query = context.Set<TEntity>();
			return withAsNoTracking ? await query.AsNoTracking().ToListAsync() : await query.ToListAsync();
		}

		public async Task<TEntity?> Get(TKey id) => await context.FindAsync<TEntity>(id);

		public async Task Add(TEntity entity) => await context.AddAsync(entity);
		
		public void Update(TEntity entity) => context.Update(entity);

		public void Delete(TEntity entity) => entity.IsDeleted = true;

	}
}
