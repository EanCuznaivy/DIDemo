using System;
using System.Runtime.CompilerServices;
using Demo.BlockChainServices;

namespace Demo.PureDIUsage
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message, [CallerMemberName] string caller = null)
        {
            Console.WriteLine(message);
        }
    }
}