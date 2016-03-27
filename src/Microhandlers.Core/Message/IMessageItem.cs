using System.Collections;
using System.Collections.Generic;

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
        IEnumerable<IMessageItem> Properties { get; }
    }

    public interface IMessageItem
    {
        string Name { get; }
        ItemType Type { get; }
        bool ValueAsBool();
        string ValueAsString();
        IItemObject ValueAsObject();
        IEnumerable<IMessageItem> ValueAsArray();

    }
}