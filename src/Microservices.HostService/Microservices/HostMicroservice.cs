using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Core;

namespace Microservices.HostService.Microservices
{
    public class HostMicroservice
    {
	    public class SomeClass
	    {
		    public string Param1 { get; set; }
			public int Param2 { get; set; }
	    }
	    public async Task<SomeClass> Index(int param1)
	    {
		    return new SomeClass() {Param1 = param1 + "_asdf", Param2 = param1+23};
	    }
    }
}
