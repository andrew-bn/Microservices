using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;

namespace Microservices.Core
{
	public class MicroservicesDispatcher : IMessageDestination
	{
		private readonly MicroservicesOptions _options;
		private readonly IServiceProvider _serviceProvider;
		private readonly IMicroservicesLocator _microservicesLocator;
		private MicroserviceHolder[] _microservices;
		private readonly SynchronizationContext _synchronizationContext;

		public MicroservicesDispatcher(IServiceProvider serviceProvider, IOptions<MicroservicesOptions> options, IMicroservicesLocator microservicesLocator)
		{
			_options = options.Value;
			_serviceProvider = serviceProvider;
			_microservicesLocator = microservicesLocator;
		
		//	_synchronizationContext = new MicroservicesSynchronizationContext();
		}

		public async Task Process(IMessageContext messageContext)
		{
			if (messageContext == null)
				throw new ArgumentNullException(nameof(messageContext));

			_microservices[0].SynchronizationContext.Post(s=>  _microservices[0].Call(messageContext.Request.MicroserviceMethod),null);

			await messageContext.Response.WriteString($"{messageContext.Request.MicroserviceName}.{messageContext.Request.MicroserviceMethod}");
		}

		public void Initialize()
		{
			_microservices = _microservicesLocator.FindMicroservices().Select(InitializeMicroservice).ToArray();
		}

		private MicroserviceHolder InitializeMicroservice(Type type)
		{
			return new MicroserviceHolder(type, _serviceProvider, _synchronizationContext);
		}

	}
}
