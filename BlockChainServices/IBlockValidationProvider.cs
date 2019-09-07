using Demo.BlockEntities;

namespace Demo.BlockChainServices
{
    public interface IBlockValidationProvider
    {
        BlockForValidation ValidateBlockBeforeAppend(IBlock block);
    }
}