using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;

namespace Microservices.Core
{
	public class MicroservicesHost : IMicroservicesHost
	{
		private readonly MicroservicesOptions _options;
		private readonly IMicroservicesFactory _microservicesFactory;
		private readonly IEventsHandler _eventsHandler;
		private Dictionary<string, IMicroservice> _microservices;
		public IMicroservice DefaultMicroservice { get; set; }

		public dynamic DynamicProxy { get { return new DynamicProxy(this); } }

		public MicroservicesHost(IOptions<MicroservicesOptions> options, IMicroservicesFactory microservicesFactory)
		{
			_options = options.Value;
			_microservicesFactory = microservicesFactory;

		}

		public async Task Process(IMessageContext messageContext)
		{
			if (messageContext == null)
				throw new ArgumentNullException(nameof(messageContext));

			var srv = FindMicroservice(messageContext);

			await srv.Invoke(messageContext.Request.MessageName, messageContext);
		}

		private IMicroservice FindMicroservice(IMessageContext msgContext)
		{
			IMicroservice srv;
			if (!_microservices.TryGetValue(msgContext.Request.Microservice.ToLower(), out srv))
				srv = DefaultMicroservice;
			if (srv == null)
				throw new MicroservicesException(MicroservicesError.MicroserviceNotFound, msgContext);

			return srv;


		}
		public void Initialize()
		{
			_microservices = _microservicesFactory.LocateMicroservices(this)
				.ToDictionary(s => s.Name.ToLower(), s => s);
		}

	}
}
