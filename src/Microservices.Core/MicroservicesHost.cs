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

		public async Task<IMessage> Handle(IMessage message)
		{
			if (message == null)
				throw new ArgumentNullException(nameof(message));

			var srv = FindMicroservice(message);

			return await srv.Invoke(message);
		}

		private IMicroservice FindMicroservice(IMessage message)
		{
			var microserviceName = message.Name.Split('.').Last().ToLower();
			IMicroservice srv;
			if (!_microservices.TryGetValue(microserviceName, out srv))
				srv = DefaultMicroservice;
			if (srv == null)
				throw new MicroservicesException(MicroservicesError.MicroserviceNotFound, message);

			return srv;


		}
		public void Initialize()
		{
			_microservices = _microservicesFactory.LocateMicroservices(this)
				.ToDictionary(s => s.Name.ToLower(), s => s);
		}

	}
}
