using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;

namespace Microservices.Core
{
	public class MicroservicesHost : IMessageHandlersHost
	{
		private readonly MicroservicesOptions _options;
		private List<IMessageHandler> _messageHandlers;
		public dynamic DynamicProxy { get { return new DynamicProxy(this); } }
		

		public MicroservicesHost(IOptions<MicroservicesOptions> options)
		{
			_options = options.Value;
		}

		public async Task<IMessage> Handle(IMessage message)
		{
			if (message == null)
				throw new ArgumentNullException(nameof(message));

			var handler = FindHandler(message);

			return await handler.Handle(message);
		}

		private IMessageHandler FindHandler(IMessage message)
		{
			//var microserviceName = message.Name.Split('.').Last().ToLower();
			//IMicroservice srv;
			//if (!_messageHandlers.TryGetValue(microserviceName, out srv))
			//	srv = DefaultMicroservice;
			//if (srv == null)
			//	throw new MicroservicesException(MicroservicesError.MicroserviceNotFound, message);

			//return srv;
			return null;

		}

		public Task Register(IMessageHandler handler)
		{
			throw new NotImplementedException();
		}

		public Task Unregister(string name)
		{
			throw new NotImplementedException();
		}
	}
}
