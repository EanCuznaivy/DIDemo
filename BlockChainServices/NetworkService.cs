using Demo.BlockEntities;

namespace Demo.BlockChainServices
{
    public class NetworkService : INetworkService
    {
        public ILogger Logger { get; set; }

        public NetworkService()
        {
            Logger = NullLogger.Instance;
        }

        public void BroadcastBlock(IBlock block)
        {
            Logger.Log($"[NetworkService] Broadcasting block {block}");
        }
    }
}