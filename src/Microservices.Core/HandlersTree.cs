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
		public HandlerNode(HandlerNode parent, string messageName, IMessageHandler handler)
		{
			Handler = handler;
			Parent = parent;
			MessageName = messageName;
		}
		public string MessageName { get; }
		public HandlerNode Parent { get; }
		public IMessageHandler Handler { get; }
		ConcurrentDictionary<string, HandlerNode> _tree = new ConcurrentDictionary<string, HandlerNode>();
		
		public IHandlersSequence CollectHandlersSequence(IMessage message)
		{
			throw new NotImplementedException();
		}

		public void Register(string messageName, IMessageHandler handler)
		{
			var name = messageName.Remove(0, MessageName.Length).Trim('.');

			if (string.IsNullOrEmpty(name))
				return;

			var parts = name.Split('.');
			
			if (parts.Length == 1)
			{
				_tree.TryAdd(parts[0], new HandlerNode(this, messageName,handler));
			}
			else
			{
				var node = string.IsNullOrEmpty(MessageName)
							? new HandlerNode(this, parts[0], null)
							: new HandlerNode(this, MessageName + "." + parts[0], null);

				_tree.TryAdd(parts[0], node);
				node.Register(messageName, handler);
			}
		}
    }
}
