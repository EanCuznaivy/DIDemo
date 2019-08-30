using System;
using System.Runtime.CompilerServices;
using Demo.BlockChainServices;

namespace Demo.MSDIUsage
{
    public class ConsoleLoggerWithTimestampDecorator : ILogger
    {
        private readonly ConsoleLoggerWithTimestamp _consoleLoggerWithTimestamp;

        public ConsoleLoggerWithTimestampDecorator(ConsoleLoggerWithTimestamp consoleLoggerWithTimestamp)
        {
            _consoleLoggerWithTimestamp = consoleLoggerWithTimestamp;
        }
        
        public void Log(string message, [CallerMemberName] string caller = null)
        {
            _consoleLoggerWithTimestamp.Log(message, caller);
            Console.WriteLine("-----");
        }
    }
}