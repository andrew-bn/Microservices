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
		MessageName MessageName { get; }
		IMessageHandler Handler { get; }
		bool HasSubHandlers { get; }
		IEnumerable<IHandlersTreeNode> SubHandlers { get; }
	}

	public class HandlerNode: IHandlersTreeNode
	{
		public HandlerNode(HandlerNode parent, MessageName messageName, IMessageHandler handler)
		{
			Handler = handler;
			Parent = parent;
			MessageName = messageName;
		}

		public MessageName MessageName { get; }
		public HandlerNode Parent { get; }
		public IMessageHandler Handler { get; }
		public bool HasSubHandlers => SubHandlers != null && SubHandlers.Any();
		public IEnumerable<IHandlersTreeNode> SubHandlers => _tree.Values;
		private readonly ConcurrentDictionary<MessageName, HandlerNode> _tree = new ConcurrentDictionary<MessageName, HandlerNode>();


		public IHandlersQueue CollectHandlersQueue(MessageName message)
		{
			var queue = new HandlersQueue();
			CollectHandlersQueue(message, queue);
			return queue;
		}

		public void CollectHandlersQueue(MessageName message, HandlersQueue queue)
		{
			if (Handler!=null)
				queue.Enqueue(Handler);

			var nextHandlerName = message.GetNextHandlerName(MessageName);
			HandlerNode node;
			if (_tree.TryGetValue(nextHandlerName, out node))
			{
				node.CollectHandlersQueue(message, queue);
			}
		}

		public void Register(MessageName messageName, IMessageHandler handler)
		{
			var next = messageName.GetNextHandlerName(MessageName);

			if (next == MessageName.Empty)
				return;

			if (next == messageName)
				_tree.TryAdd(next, new HandlerNode(this, next, handler));
			else
			{
				var node = new HandlerNode(this, next, null);
				_tree.GetOrAdd(next, node).Register(messageName, handler);
			}
		}
	}

}
