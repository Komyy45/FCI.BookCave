using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCI.BookCave.Domain.Enums
{
	public enum OrderStatus : byte
	{
		Pending,
		PaymentFailed,
		PaymentReceived
	}
}
