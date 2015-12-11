using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microservices.Core.Messaging
{
	public class TypeBasedSchema : IMessageParameterSchema
	{
		public Type UnderlyingType { get; }
		public string ParameterName { get; }
		public IEnumerable<IMessageParameterSchema> Parameters { get; }
		public ParameterType Type { get; }

		public TypeBasedSchema(string name, Type type)
		{
			UnderlyingType = type;
			ParameterName = name;
			Parameters = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(p => new TypeBasedSchema(p.Name, p.PropertyType)).ToArray();
			Type = ParameterType.Object;
			if (type == typeof(string))
				Type = ParameterType.String;
			if (type == typeof(bool))
				Type = ParameterType.Integer;
			if (type == typeof(float) || type == typeof(double) || type == typeof(decimal))
				Type = ParameterType.Real;
			if (type == typeof(int) || type == typeof(long) || type == typeof(short) || type == typeof(byte) ||
				type == typeof(uint) || type == typeof(ulong) || type == typeof(ushort))
				Type = ParameterType.Integer;
			if (type.IsArray || typeof(IEnumerable).IsAssignableFrom(type))
				Type = ParameterType.Array;
		}
	}
}