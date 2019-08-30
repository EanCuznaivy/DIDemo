using System;
using System.Runtime.CompilerServices;
using Demo.BlockChainServices;

namespace Demo.MSDIUsage
{
    public class ConsoleLoggerWithTimestamp : ILogger
    {
        public void Log(string message, [CallerMemberName] string caller = null)
        {
            Console.WriteLine($"Caller: {caller}\n[{DateTime.UtcNow:HH:mm:ss.ffff}] {message}");
        }
    }
}