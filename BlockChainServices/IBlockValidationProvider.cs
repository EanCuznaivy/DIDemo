using Demo.BlockEntities;

namespace Demo.BlockChainServices
{
    public interface IBlockValidationProvider
    {
        bool ValidateBlockBeforeAppend(IBlock block);
    }
}