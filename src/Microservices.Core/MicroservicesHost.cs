using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.OptionsModel;
using Microsoft.Extensions.PlatformAbstractions;

namespace Microservices.Core
{
	public class MicroservicesHost : IMessageDestination
	{
		private readonly MicroservicesOptions _options;

		public MicroservicesHost(IOptions<MicroservicesOptions> options)
		{
			_options = options.Value;
		}

		public async Task Process(IMessageContext messageContext)
		{
            if (messageContext == null)
                throw new ArgumentNullException(nameof(messageContext));

		    await messageContext.Response.WriteString("hello!");
		}

        public void Initialize()
        {
			
            
        }
    }
}
