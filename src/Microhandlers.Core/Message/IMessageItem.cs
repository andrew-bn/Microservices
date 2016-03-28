using System.Collections;
using System.Collections.Generic;
using Microhandlers.Core.Objects;

namespace Microhandlers.Core.Message
{
    /// <summary>
    /// http://www.json.org/
    /// </summary>
    public enum ItemType
    {
        Object,
        Array,
        String,
        IntegerNumber,
        FloatNumber,
        Boolean,
        Null
    }

    public interface IItemObject
    {
        IDictionary<InsensitiveString, IMessageItem> Properties { get; }
    }

    public interface IMessageItem
    {
		InsensitiveString Name { get; }
        ItemType Type { get; }
        bool ValueAsBool();
        string ValueAsString();
        IItemObject ValueAsObject();
        IEnumerable<IMessageItem> ValueAsArray();

    }
}