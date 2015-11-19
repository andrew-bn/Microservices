using System;
using System.Collections.Generic;
using System.Linq;

namespace Microservices.Core
{
    public class MicroservicesHost: IMessageDestination
    {
        public MicroservicesHost()
        {

        }

		public void Receive(Message message)
		{
			throw new NotImplementedException();
		}
	}
}
