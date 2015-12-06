using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Core.Messaging;
using Microsoft.Extensions.OptionsModel;

namespace Microservices.Core
{
	public class MicroservicesHost : IMessageHandler, IMessageHandlersHost
	{
		public string HostName => "Microhost";
		public string Name => string.Empty;
		public string Version => "1.0.0";
		public IMessageSchema Message => null;
		public IMessageSchema Response => null;

		private readonly MicroservicesOptions _options;
		private readonly Dictionary<Type, object> _serviceLocator = new Dictionary<Type, object>();
		private readonly IServiceProvider _serviceProvider;
		private HandlerNode _handlersTree;
		public MicroservicesHost(IOptions<MicroservicesOptions> options, IServiceProvider serviceProvider)
		{
			_options = options.Value;
			_serviceProvider = serviceProvider;
			_handlersTree = new HandlerNode(null, this);
		}

		public Task<IMessage> Handle(IMessage message)
		{
			if (message == null)
				throw new ArgumentNullException(nameof(message));
			var sequence = _handlersTree.CollectHandlersSequence(message);

			return ((IMessageHandler)this).Handle(this, message, sequence);
		}

		Task<IMessage> IMessageHandler.Handle(IMessageHandlersHost host, IMessage message, IHandlersSequence sequence)
		{
			return sequence.Next(this, message).Handle(host, message, sequence);
		}

		public void AddDependency<T>(T implementation)
		{
			_serviceLocator.Add(typeof(T), implementation);
		}

		public object ResolveDependency(Type type)
		{
			var srv = _serviceProvider.GetService(type);
			if (srv == null)
				_serviceLocator.TryGetValue(type, out srv);
			return srv;
		}

		public void Register(IMessageHandler handler)
		{
			_handlersTree.Register(handler);
		}

	}
}
