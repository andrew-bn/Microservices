namespace Microservices.Core
{
    public interface IMessageContext
    {
		IMicroservicesHost Host { get; }

		IMessage Request { get; }
        IMessageSource Source { get; }
	}
}