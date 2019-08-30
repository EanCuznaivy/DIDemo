using System;
using Demo.BlockChainServices;

namespace Demo.PureDIUsage
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}