using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microhandlers.Core.Message;

namespace Microhandlers.HandlersDiscovery.Microservice
{
    /// <summary>
    /// This class is used do deserialize message to .NET types
    /// </summary>
    public class HandlerParameterDeserializer
    {
        public object Deserialize(Type type, IMessageItem message)
        {
            if (message.Type == ItemType.Boolean)
            {
                var value = message.ValueAsBool();
                var intVal = value ? 1 : 0;
                if (type == typeof (bool)) return value;
                if (type == typeof (string)) return value.ToString();
                if (type == typeof (int)) return intVal;
                if (type == typeof(byte)) return (byte)intVal;
                if (type == typeof(sbyte)) return (sbyte)intVal;
                if (type == typeof(short)) return (short)intVal;
                if (type == typeof(ushort)) return (ushort)intVal;
                if (type == typeof(long)) return (long)intVal;
                if (type == typeof(ulong)) return (ulong)intVal;
            }
			else if (message.Type == ItemType.IntegerNumber)
			{
				var value = long.Parse(message.ValueAsString());
				if (type == typeof(bool)) return value==0?false:true;
				if (type == typeof(string)) return value.ToString();
				if (type == typeof(int)) return (int)value;
				if (type == typeof(byte)) return (byte)value;
				if (type == typeof(sbyte)) return (sbyte)value;
				if (type == typeof(short)) return (short)value;
				if (type == typeof(ushort)) return (ushort)value;
				if (type == typeof(long)) return (long)value;
				if (type == typeof(ulong)) return (ulong)value;
			}
            else if (message.Type == ItemType.Null)
            {
                return null;
            }
            else if (message.Type == ItemType.String)
                ;
            return null;
            //  var result = Activator.CreateInstance(type);



            //return result;
        }

        private object DeserializeItem(IMessageItem item)
        {
            return null;
        }

      //  private bool DeserializeBool
    }
}
