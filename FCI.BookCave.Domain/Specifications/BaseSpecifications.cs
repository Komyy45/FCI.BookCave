using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Contracts.Specification;
using FCI.BookCave.Domain.Entities.Common;

namespace FCI.BookCave.Domain.Specifications
{
	public class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity,TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public Expression<Func<TEntity, bool>> Criteria { get; set; } = null!;

		public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new();

		public bool IsPaginationEnabled { get; set; }

		public int Take { get; set; }

		public int Skip { get; set; }

		public BaseSpecifications()
		{}

		public BaseSpecifications(TKey id) : this(e => e.Id.Equals(id))
		{ }

		public BaseSpecifications(Expression<Func<TEntity, bool>> criteria)
		{
			Criteria = criteria;
		}

		public virtual void AddIncludes() { }

		public virtual void ApplyPagination(int take, int skip)
		{
			IsPaginationEnabled = true;
			Take = take;
			Skip = skip;
		}
	}
}
