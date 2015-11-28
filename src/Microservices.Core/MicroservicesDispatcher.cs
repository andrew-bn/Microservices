using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;

namespace Microservices.Core
{
	public class MicroservicesDispatcher : IMicroservicesDispatcher
	{
		private readonly MicroservicesOptions _options;
		private readonly IMicroservicesLocator _microservicesLocator;
		private Dictionary<string, IMicroservice> _microservices;

		public MicroservicesDispatcher(IOptions<MicroservicesOptions> options, IMicroservicesLocator microservicesLocator)
		{
			_options = options.Value;
			_microservicesLocator = microservicesLocator;
		}

		public async Task Process(IMessageContext messageContext)
		{
			if (messageContext == null)
				throw new ArgumentNullException(nameof(messageContext));

			IMicroservice srv;
			if (!_microservices.TryGetValue(messageContext.Request.MicroserviceName.ToLower(), out srv))
				throw new MicroservicesException(MicroservicesError.MicroserviceNotFound, messageContext);
			
			await srv.Invoke(messageContext.Request.MicroserviceMethod, messageContext);
		}

		public void Initialize()
		{
			_microservices = _microservicesLocator.LocateMicroservices()
				.ToDictionary(s => s.Name.ToLower(), s => s);
		}

	}
}
