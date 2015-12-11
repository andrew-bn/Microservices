using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Core.Messaging;
using Microsoft.Extensions.OptionsModel;

namespace Microservices.Core
{
	public class MicroservicesHost : IMessageHandlersHost
	{
		public string HostName => "Microhost";
		public string Version => "1.0.0";
		public IMessageValueSchema Message => null;
		public IMessageValueSchema Response => null;

		private readonly MicroservicesOptions _options;
		private readonly Dictionary<Type, object> _serviceLocator = new Dictionary<Type, object>();
		private readonly IServiceProvider _serviceProvider;
		public IHandlersTreeNode HandlersTree => _handlersTree;
		private HandlerNode _handlersTree;
		public MicroservicesHost(IOptions<MicroservicesOptions> options, IServiceProvider serviceProvider)
		{
			_options = options.Value;
			_serviceProvider = serviceProvider;
			_handlersTree = new HandlerNode(null, string.Empty, this);
		}

		public Task<IMessage> Handle(IMessage message)
		{
			if (message == null)
				throw new ArgumentNullException(nameof(message));

			var sequence = _handlersTree.CollectHandlersQueue(message.Name);

			return sequence.Next().Handle(this, message, sequence);
		}

		public DynamicProxy CreateDynamicProxy()
		{
			return new DynamicProxy(this);
		}

		Task<IMessage> IMessageHandler.Handle(IMessageHandlersHost host, IMessage message, IHandlersQueue sequence)
		{
			return sequence.Next().Handle(host, message, sequence);
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

		public void Register(string messageName, IMessageHandler handler)
		{
			_handlersTree.Register(messageName, handler);
		}

	}
}
