using System.Runtime.CompilerServices;

namespace Demo.BlockChainServices
{
    // 前置条件检查
    // 后置条件检查
    // 不变式检查（AOP）
    public interface ILogger
    {
        void Log(string message, [CallerMemberName] string caller = null);
    }

    public class NullLogger : ILogger
    {
        public static readonly NullLogger Instance = new NullLogger();

        public void Log(string message, [CallerMemberName] string caller = null)
        {
            // Do nothing.
        }
    }
}