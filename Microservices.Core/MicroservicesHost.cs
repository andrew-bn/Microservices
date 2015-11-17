using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Core
{
    public abstract class Message
    {

    }
    public interface MessageSource
    {
        Task<Message> Next();
    }
    public class MicroservicesHost
    {
        public MicroservicesHost()
        {
        }

        
    }
}
