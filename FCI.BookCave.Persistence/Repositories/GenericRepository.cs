using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Contracts.GenericRepository;
using FCI.BookCave.Domain.Contracts.Specification;
using FCI.BookCave.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace FCI.BookCave.Persistence.Repositories
{
	public class GenericRepository<TEntity, TKey>(DbContext context) : IGenericRepository<TEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public async Task<IEnumerable<TEntity>> GetAll(bool withAsNoTracking = true)
		{
			var query = context.Set<TEntity>();
			return withAsNoTracking ? await query.AsNoTracking().ToListAsync() : await query.ToListAsync();
		}

		public async Task<IEnumerable<TEntity>> GetAll(ISpecifications<TEntity, TKey> specs, bool withAsNoTracking = true)
		{
			IQueryable<TEntity> query = context.Set<TEntity>();

			if (withAsNoTracking) query = query.AsNoTracking();

			return await GetQuery(query, specs).ToListAsync();
		}

		public async Task<TEntity?> Get(TKey id) => await context.FindAsync<TEntity>(id);

		public async Task<TEntity?> Get(ISpecifications<TEntity, TKey> specs, TKey id)
			=> await GetQuery(context.Set<TEntity>(), specs).FirstOrDefaultAsync(e => e.Id.Equals(id));

		public async Task Add(TEntity entity) => await context.AddAsync(entity);
		
		public void Update(TEntity entity) => context.Update(entity);

		public void Delete(TEntity entity) => entity.IsDeleted = true;

		public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null!) 
			=> predicate is null ? await context.Set<TEntity>().CountAsync() : await context.Set<TEntity>().CountAsync(predicate);

		private IQueryable<TEntity> GetQuery(IQueryable<TEntity> query, ISpecifications<TEntity,TKey> spec)
		{
			return SpecificationEvaluator.GetQuery(query, spec);
		}
	}
}
