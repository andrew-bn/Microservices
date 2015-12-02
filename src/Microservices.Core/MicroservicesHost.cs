using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.OptionsModel;

namespace Microservices.Core
{
	public class MicroservicesHost : IMessageHandlersHost
	{
		private readonly MicroservicesOptions _options;
		private readonly List<IMessageHandler> _messageHandlers = new List<IMessageHandler>();
		private readonly Dictionary<Type, object> _serviceLocator = new Dictionary<Type, object>();
		private readonly IServiceProvider _serviceProvider;

		public IEnumerable<IMessageHandler> MessageHandlers => _messageHandlers;

		public MicroservicesHost(IOptions<MicroservicesOptions> options, IServiceProvider serviceProvider)
		{
			_options = options.Value;
			_serviceProvider = serviceProvider;
			Name = nameof(MicroservicesHost);
			Version = "1.0.0";
		}

		public async Task<IMessage> Handle(IMessage message)
		{
			if (message == null)
				throw new ArgumentNullException(nameof(message));

			var handler = FindHandler(message);

			return await handler.Handle(this, message);
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

		private IMessageHandler FindHandler(IMessage message)
		{
			var handler = _messageHandlers.FirstOrDefault(mh => mh.CatchPattern == message.Name);
			if (handler == null)
				throw new MicroservicesException(MicroservicesError.MicroserviceNotFound, message);
			return handler;
		}

		public void Register(IMessageHandler handler)
		{
			_messageHandlers.Add(handler);
		}

		public void Unregister(string name)
		{
			throw new NotImplementedException();
		}

		public string Name { get; }
		public string Version { get; }

		
		public async Task Initialize()
		{
			foreach (var eh in _messageHandlers.Where(eh => eh.CatchPattern.EndsWith(".initialize")))
			{
				await eh.Handle(this, new EmptyMessage(eh.CatchPattern));
			}
		}
	}
}
