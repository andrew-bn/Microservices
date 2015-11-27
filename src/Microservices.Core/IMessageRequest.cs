using System;

namespace Microservices.Core
{
    public interface IMessageRequest
    {
        string MicroserviceName { get; }
        string MicroserviceMethod { get; }

		object ReadParameter(Type type, RequestParameter parameter);
	}
}