using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Abstractions.Models.Identity;
using FCI.BookCave.Domain.Entities.Identity;
using Microsoft.AspNetCore.Builder.Extensions;
using Riok.Mapperly.Abstractions;

namespace FCI.BookCave.Application.Mapping
{
	[Mapper]
	public partial class MapperlyMapper
	{		
		public static partial AuthDto ToDto(ApplicationUser entity, string token, DateTime ExpiresOn);
		public static partial ApplicationUser ToEntity(RegisterDto entity);
	}
}
