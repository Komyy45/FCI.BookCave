﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCI.BookCave.Abstractions.Models.Identity
{
	public record RefreshTokenDto(string Token, DateTime ExpiresOn);

}