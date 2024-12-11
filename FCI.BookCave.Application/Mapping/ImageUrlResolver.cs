using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FCI.BookCave.Application.Mapping
{
	public class ImageUrlResolver(IConfiguration configuration)
	{
		public string Resolve(string pictureUrl)
		{
			return $"{configuration["BaseUrls:ApiBaseUrl"]}/images/{pictureUrl}";
		}
	}
}
