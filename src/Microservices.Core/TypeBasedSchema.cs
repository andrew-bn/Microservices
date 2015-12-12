using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microservices.Core
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

			Parameters = GetParameters(type);
			Type = type.ToParameterType();
		}

		private static IEnumerable<IMessageParameterSchema> GetParameters(Type type)
		{
			if (type.IsPrimitive())
				return new IMessageParameterSchema[0];
			return
				type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
					.Select(p => new TypeBasedSchema(p.Name, p.PropertyType))
					.ToArray();
		}
	}
}