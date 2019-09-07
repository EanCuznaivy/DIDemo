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

        // [OCP]
        // 对ConsoleLoggerWithTimestamp进行了扩展，而不用修改ConsoleLoggerWithTimestamp本身的代码。
        public void Log(string message, [CallerMemberName] string caller = null)
        {
            _consoleLoggerWithTimestamp.Log(message, caller);
            Console.WriteLine("-----");
        }
    }
}