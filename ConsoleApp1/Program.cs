using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
			var th = new Thread(() => Console.WriteLine(""));
			th.Start();
        }
    }
}
