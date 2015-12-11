using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Core
{
    public struct MessageName
    {
		public static MessageName Empty = new MessageName(string.Empty);

		private string[] _parts;
		private string _name;
		public MessageName(string name)
		{
			if (name == null)
				throw new ArgumentNullException(nameof(name));

			_parts = name.ToLower().Split('.');
			_name = name;
		}

		public MessageName GetNextHandlerName(MessageName currentName)
		{
			var nextHandlerName = string.Empty;

			for(var i = 0; i < _parts.Length; i++)
			{

				if (nextHandlerName.Length > 0)
					nextHandlerName += ".";
				nextHandlerName += _parts[i];

				if (i == currentName._parts.Length)
					break;
				else if (currentName._parts[i] != _parts[i])
					return Empty;
			}

			if (((MessageName)nextHandlerName).Equals(currentName))
				return Empty;

			return nextHandlerName;
		}

		public static implicit operator string(MessageName message)
		{
			return message._name;
		}

		public static implicit operator MessageName(string message)
		{
			return new MessageName(message);
		}

		public override int GetHashCode()
		{
			return _name.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is MessageName)) return false;
			var name = (MessageName)obj;

			return name._name.Equals(_name, StringComparison.OrdinalIgnoreCase);
		}
	}
}
