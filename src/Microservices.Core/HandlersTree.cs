using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Core.Messaging;

namespace Microservices.Core
{
	public interface IHandlersSequence
	{
		IMessageHandler Next(IMessageHandler current, IMessage message);
	}
    public class HandlerNode
	{
		public class EmptyMessageHandler : IMessageHandler
		{
			public EmptyMessageHandler(string name)
			{
				Name = name;
			}

			public IMessageSchema Message { get; }
			public string Name { get; }
			public IMessageSchema Response { get; }

			public Task<IMessage> Handle(IMessageHandlersHost host, IMessage message, IHandlersSequence sequence)
			{
				return sequence.Next(this, message).Handle(host,message, sequence);
			}
		}

		public HandlerNode(HandlerNode parent, IMessageHandler handler)
		{
			Handler = handler;
			Parent = parent;
		}

		public HandlerNode Parent { get; }
		public IMessageHandler Handler { get; }
		ConcurrentDictionary<string, HandlerNode> _tree = new ConcurrentDictionary<string, HandlerNode>();
		
		public IHandlersSequence CollectHandlersSequence(IMessage message)
		{
			throw new NotImplementedException();
		}

		public void Register(IMessageHandler handler)
		{
			var name = handler.Name.Remove(0, Handler.Name.Length).Trim('.');

			if (string.IsNullOrEmpty(name))
				return;

			var parts = name.Split('.');
			
			if (parts.Length == 1)
			{
				_tree.TryAdd(parts[0], new HandlerNode(this,handler));
			}
			else
			{
				var node = string.IsNullOrEmpty(Handler.Name)
							? new HandlerNode(this, new EmptyMessageHandler(parts[0]))
							: new HandlerNode(this, new EmptyMessageHandler(Handler.Name + "." + parts[0]));

				_tree.TryAdd(parts[0], node);
				node.Register(handler);
			}
		}
    }
}
