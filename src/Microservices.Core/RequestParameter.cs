using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Core
{
	public struct RequestParameter
	{
		public readonly int Index;
		public readonly string Name;
		public RequestParameter(int index, string name)
		{
			Index = index;
			Name = name;
		}
	}
}
