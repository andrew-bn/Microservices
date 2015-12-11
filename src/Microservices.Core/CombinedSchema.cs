using System;
using System.Collections.Generic;
using System.Linq;

namespace Microservices.Core.Messaging
{
	public class CombinedSchema : IMessageSchema
	{
		private readonly IMessageSchema[] _schemas;

		public CombinedSchema(string name, params IMessageSchema[] schemas)
		{
			Name = name;
			_schemas = schemas;
		}

		public MessageName Name { get; }
		public ParameterType Type { get {return ParameterType.Object;} }

		public IEnumerable<IMessageParameterSchema> Parameters
		{
			get { return _schemas.SelectMany(s => s.Parameters).ToArray(); }
		}
	}
}