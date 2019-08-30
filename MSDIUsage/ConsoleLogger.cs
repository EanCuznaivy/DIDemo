using System;
using System.Runtime.CompilerServices;
using Demo.BlockChainServices;

namespace Demo.MSDIUsage
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message, [CallerMemberName] string caller = null)
        {
            Console.WriteLine($"{caller}\n{message}\n");
        }
    }
}