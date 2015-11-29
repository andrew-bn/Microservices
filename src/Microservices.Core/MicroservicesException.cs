using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Core
{
	public class MicroservicesException : Exception
	{
		public MicroservicesError Error { get; }

		public MicroservicesException(MicroservicesError error, IMessage message)
		{
			Error = error;
		}
	}
}
