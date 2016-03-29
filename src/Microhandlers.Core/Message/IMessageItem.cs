using System.Collections;
using System.Collections.Generic;
using Microhandlers.Core.Objects;

namespace Microhandlers.Core.Message
{
	public interface IMessageItem
	{
		ItemType Type { get; }
		bool ValueAsBool();
		string ValueAsString();
		IItemObject ValueAsObject();
		IEnumerable<IMessageItem> ValueAsArray();
	}
}