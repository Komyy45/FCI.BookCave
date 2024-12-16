using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Contracts.Specification;
using FCI.BookCave.Domain.Entities.Common;
using FCI.BookCave.Domain.Entities.Products;
using FCI.BookCave.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace FCI.BookCave.Persistence.Repositories
{
	internal static class SpecificationEvaluator

	{
		public static IQueryable<TEntity> GetQuery<TEntity, TKey>(IQueryable<TEntity> query, ISpecifications<TEntity, TKey> specs)
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
		{
			if (specs is null || query is null) return query!;

			if (specs.Criteria is not null) query = query.Where(specs.Criteria);

			if (specs.Includes.Count > 0) query = specs.Includes.Aggregate(query, (acc,cur) => query.Include(cur));

			if (specs.IsPaginationEnabled)
				query = query.Skip(specs.Skip).Take(specs.Take);

			return query;
		}
	}
}
