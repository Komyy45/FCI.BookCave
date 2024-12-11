using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCI.BookCave.Abstractions.Contracts;
using FCI.BookCave.Abstractions.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FCI.BookCave.Controllers.Controllers.Common
{
	[ApiController]
	[Route("/api/[controller]")]
	public class BaseApiController : ControllerBase
	{

	}
}
