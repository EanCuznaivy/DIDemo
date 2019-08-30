using Demo.BlockEntities;

namespace Demo.BlockChainServices
{
    public interface IBlockChainService
    {
        bool AppendBlock(IBlock block);
        Hash GetLatestBlockHash();
        long GetCurrentHeight();
    }
}