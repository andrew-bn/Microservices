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

		public string Name { get; }
		public ParameterType Type { get {return ParameterType.Object;} }

		public IEnumerable<IMessageSchema> Parameters
		{
			get { return _schemas.SelectMany(s => s.Parameters).ToArray(); }
		}
	}
}