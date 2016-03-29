using System.Collections.Generic;
using Microhandlers.Core.Objects;

namespace Microhandlers.Core.Message
{
	public interface IItemObject
	{
		IMessageItem this[InsensitiveString propertyName] { get; }
		IEnumerable<KeyValuePair<InsensitiveString, IMessageItem>> Enumerate();
	}
}