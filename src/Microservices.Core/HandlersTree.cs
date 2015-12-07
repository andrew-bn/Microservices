using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Core.Messaging;

namespace Microservices.Core
{
	public interface IHandlersQueue
	{
		IMessageHandler Next();
	}
	public class HandlersQueue : Queue<IMessageHandler>, IHandlersQueue
	{
		public IMessageHandler Next()
		{
			return Dequeue();
		}
	}
	public interface IHandlersTreeNode
	{
		string MessageName { get; }
		IMessageHandler Handler { get; }
		IEnumerable<IHandlersTreeNode> SubHandlers { get; }
	}
	public class HandlerNode: IHandlersTreeNode
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
		public IEnumerable<IHandlersTreeNode> SubHandlers => _tree.Values;
		ConcurrentDictionary<string, HandlerNode> _tree = new ConcurrentDictionary<string, HandlerNode>();


		public IHandlersQueue CollectHandlersQueue(string msgName, IMessage message)
		{
			var queue = new HandlersQueue();
			CollectHandlersQueue(msgName, message, queue);
			return queue;
		}

		public void CollectHandlersQueue(string msgName, IMessage message, HandlersQueue queue)
		{
			if (Handler!=null)
				queue.Enqueue(Handler);

			var name = message.Name.Remove(0, msgName.Length).Trim('.');
			if (string.IsNullOrEmpty(name))
				return;

			if (msgName.Length > 0)
				msgName += ".";

			var parts = name.Split('.');
			HandlerNode node;
			if (_tree.TryGetValue(parts[0], out node))
			{
				node.CollectHandlersQueue(msgName + parts[0], message, queue);
			}
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
