using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FCI.BookCave.Persistence
{
	public static class AssemblyInformation
	{
        public static Assembly Assembly =>  typeof(AssemblyInformation).Assembly;
    }
}
