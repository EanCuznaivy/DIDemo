using System.Runtime.CompilerServices;

namespace Demo.BlockChainServices
{
    // 契约式设计三个要点：
    // 前置条件检查
    // 后置条件检查
    // 不变式检查（AOP）
    // [LSP]
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