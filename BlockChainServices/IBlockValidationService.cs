using Demo.BlockEntities;

namespace Demo.BlockChainServices
{
    public interface IBlockValidationService
    {
        bool ValidateBlockBeforeAppend(IBlock block);
    }
}