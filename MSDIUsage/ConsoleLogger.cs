using System;
using Demo.BlockChainServices;

namespace Demo.MSDIUsage
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"{message}\n------------\n");
        }
    }
}