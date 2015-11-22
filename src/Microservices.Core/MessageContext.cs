namespace Microservices.Core
{
    public interface IMessageContext
    {
        IMessageRequest Request { get; }
        MessageSource Source { get; }
        IMessageResponse Response { get; }
    }
}