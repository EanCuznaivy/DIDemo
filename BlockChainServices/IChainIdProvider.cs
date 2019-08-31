using Demo.BlockEntities;

namespace Demo.BlockChainServices
{
    public interface IChainIdProvider
    {
        Hash ChainId { get; }
    }
}