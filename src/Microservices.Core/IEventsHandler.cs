using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Core
{
	public interface IEventsHandler
	{
		void Handle(IMicroservice microservice, IMicroserviceEvent @event, EventArgs arguments);
	}
}
