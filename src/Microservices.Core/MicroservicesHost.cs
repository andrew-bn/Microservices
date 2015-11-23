using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;

namespace Microservices.Core
{
	public class MicroservicesHost : IMessageDestination
	{
		private readonly MicroservicesOptions _options;
		private IMicroservicesLocator _microservicesLocator;
		private Type[] _microservices;

		public MicroservicesHost(IServiceCollection serviceCollection, IOptions<MicroservicesOptions> options, IMicroservicesLocator microservicesLocator)
		{
			_options = options.Value;
			_microservicesLocator = microservicesLocator;
		}

		public async Task Process(IMessageContext messageContext)
		{
            if (messageContext == null)
                throw new ArgumentNullException(nameof(messageContext));

			var syncContext = SynchronizationContext.Current;

			await messageContext.Response.WriteString($"{messageContext.Request.MicroserviceName}.{messageContext.Request.MicroserviceMethod}");
		}

        public void Initialize()
        {
			_microservices = _microservicesLocator.FindMicroservices().Select(InitializeMicroservice).ToArray();
        }

		private Type InitializeMicroservice(Type m)
		{
			return m;
		}
	}
}
