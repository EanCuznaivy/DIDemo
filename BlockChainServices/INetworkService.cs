using Demo.BlockEntities;

namespace Demo.BlockChainServices
{
    public interface INetworkService
    {
        void BroadcastBlock(IBlock block);
    }
}