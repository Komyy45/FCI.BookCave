using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Domain.Entities.Orders;

namespace FCI.BookCave.Domain.Specifications.Orders
{
	public class OrderSpecifications : BaseSpecifications<Order, int>
	{
		public OrderSpecifications(string buyerEmail): base(o => o.BuyerEmail == buyerEmail)
		{}
		public OrderSpecifications()
		{
			AddIncludes();
		}

		public override void AddIncludes()
		{
			Includes.Add(o => o.Items);
		}
	}
}
