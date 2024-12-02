﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCI.BookCave.Domain.Entities.Common
{
	public abstract class BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
        public TKey Id { get; set; }
		public bool IsDeleted { get; set; }
	}
}
