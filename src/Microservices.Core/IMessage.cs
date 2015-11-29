using System;

namespace Microservices.Core
{
    public interface IMessage
    {
        string Microservice { get; }
        string MicroserviceMethod { get; }

		object ReadParameter(Type type, RequestParameter parameter);
	}
}