using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Contracts.GenericRepository;
using FCI.BookCave.Domain.Contracts.UnitOfWork;
using FCI.BookCave.Domain.Entities.Common;
using FCI.BookCave.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FCI.BookCave.Persistence.UnitOfWork
{
	public abstract class UnitOfWork(DbContext context) : IUnitOfWork
	{
		private ConcurrentDictionary<string, object> repositories = new();

		public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
			where TEntity : BaseEntity<TKey>
			where TKey : IEquatable<TKey>
		{
			return (IGenericRepository<TEntity, TKey>)repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(context));
		}

		public async Task<int> CompleteAsync() => await context.SaveChangesAsync();

		public async ValueTask DisposeAsync() => await context.DisposeAsync();
	}
}
