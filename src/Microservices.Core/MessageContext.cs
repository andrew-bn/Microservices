namespace Microservices.Core
{
    public interface IMessageContext
    {
		IMicroservicesDispatcher Dispatcher { get; }

		IMessageRequest Request { get; }
        MessageSource Source { get; }
        IMessageResponse Response { get; }
	}
}