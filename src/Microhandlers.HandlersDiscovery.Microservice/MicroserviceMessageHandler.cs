using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microhandlers.Core.Infrastructure;
using Microhandlers.Core.Message;
using Microhandlers.Core.Objects;
using Microhandlers.Core.Extensions;

namespace Microhandlers.HandlersDiscovery.Microservice
{
	public class MicroserviceMessageHandler : IMessageHandler
	{
		private readonly Type _type;
		private readonly MethodInfo _method;
		private readonly HandlerParameterDeserializer _deserializer;

		public MessageName Name { get; }

		public MicroserviceMessageHandler(MessageName name, Type type, MethodInfo method, HandlerParameterDeserializer deserializer)
		{
			Name = name;
			_type = type;
			_method = method;
			_deserializer = deserializer;
		}

		public async Task<IMessage> Handle(IMessage message, IServicesContainer servicesContainer)
		{
			var parameters = CollectParameters(message, _deserializer, servicesContainer);

			var instance = Activator.CreateInstance(_type);

	
			var result = _method.Invoke(instance, parameters.ToArray());
			if (result == null) return null;
			if (typeof(IMessage).IsAssignableFrom(result.GetType()))
				return (IMessage)result;

			var _returnType = _method.ReturnType;
			if (_returnType.IsTask())
			{
				if (_method.ReturnType.IsGenericType())
					_returnType = _method.ReturnType.GetGenericArguments()[0];

				await ((Task)result);

				result = _method.ReturnType.IsGenericType()
							? result.GetType().GetProperty("Result").GetValue(result)
							: null;
			}

			return new ObjectBasedMessage(_returnType, message.Name, result);
		}

		private List<object> CollectParameters(IMessage message, HandlerParameterDeserializer handlerParameterDeserializer, IServicesContainer servicesContainer)
		{
			var parameters = new List<object>();
			foreach (var p in _method.GetParameters())
			{
				object value = null;
				if (p.IsRetval) continue;

				if (p.HasDefaultValue)
					value = p.DefaultValue;

				servicesContainer.TryToResolve(p.ParameterType, out value);

				if (value == null)
				{
					var obj = message.ValueAsObject();
					var msgItem = obj[p.Name];
					value = handlerParameterDeserializer.Deserialize(p.ParameterType, msgItem);
				}
				parameters.Add(value);
			}
			return parameters;
		}
	}
}
