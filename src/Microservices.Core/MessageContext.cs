namespace Microservices.Core
{
    public interface IMessageContext
    {
		IMessageHandlersHost Host { get; }

		IMessage Request { get; }
        IMessageSource Source { get; }
	}
}