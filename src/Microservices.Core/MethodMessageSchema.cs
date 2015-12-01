using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microservices.Core.Messaging
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

		public string Name { get; }
		public ParameterType Type { get { return ParameterType.Object; } }
		public IEnumerable<IMessageSchema> Parameters { get; }
	}
}