namespace Microservices.Core
{
    public interface IMessageContext
    {
		IMicroservicesHost Host { get; }

		IMessage Request { get; }
        MessageSource Source { get; }
        IMessageResponse Response { get; }
	}
}