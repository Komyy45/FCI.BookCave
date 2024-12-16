using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCI.BookCave.Domain.Entities.Orders
{
	public enum OrderStatus : byte
	{
		Pending,
		PaymentFailed,
		PaymentReceived
	}
}
