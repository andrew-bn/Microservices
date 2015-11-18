namespace Microservices.Core
{
    public interface IMessageDestination
    {
        void Receive(Message message);
    }
}