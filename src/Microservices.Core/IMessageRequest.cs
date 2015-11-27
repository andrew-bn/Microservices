namespace Microservices.Core
{
    public interface IMessageRequest
    {
        string MicroserviceName { get; }
        string MicroserviceMethod { get; }

		T ReadParameter<T>(RequestParameter parameter);
	}
}