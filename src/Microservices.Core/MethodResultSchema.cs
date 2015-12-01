using System.Reflection;

namespace Microservices.Core.Messaging
{
	public class MethodResultSchema : TypeBasedSchema
	{
		public MethodResultSchema(string name, MethodInfo methodInfo) 
			: base(name, methodInfo.ReturnType)
		{
		}
	}
}