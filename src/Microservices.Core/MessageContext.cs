namespace Microservices.Core
{
    public abstract class MessageContext
    {
        public abstract MessageSource Source { get; protected set; }
        public abstract IMessageResponse Response { get; protected set; }


    }
}