namespace Demo.BlockChainServices
{
    public interface ILogger
    {
        void Log(string message);
    }

    public class NullLogger : ILogger
    {
        public static readonly NullLogger Instance = new NullLogger();

        public void Log(string message)
        {
            // Do nothing.
        }
    }
}