using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microservices.Core
{
	public class MethodMessageSchema : IMessageSchema
	{
		private readonly MethodInfo _methodInfo;

		public MethodMessageSchema(string name, MethodInfo methodInfo)
		{
			_methodInfo = methodInfo;
			Name = name;
			Parameters = methodInfo.GetParameters()
				.Select(p => new TypeBasedSchema(p.Name, p.ParameterType)).ToArray();
		}

		public MessageName Name { get; }
		public ParameterType Type => ParameterType.Object;
		public IEnumerable<IMessageParameterSchema> Parameters { get; }
	}
}