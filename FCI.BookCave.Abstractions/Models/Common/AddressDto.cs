﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCI.BookCave.Abstractions.Models.Common
{
	public class AddressDto
	{
		public string FirstName { get; set; } = null!;

		public string LastName { get; set; } = null!;

		public string Country { get; set; } = null!;

		public string City { get; set; } = null!;

		public string Street { get; set; } = null!;
	}
}
