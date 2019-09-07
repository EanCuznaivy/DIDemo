using Demo.BlockEntities;

namespace Demo.BlockChainServices
{
    public class BlockBasicInformationValidationProvider : IBlockValidationProvider
    {
        public ILogger Logger { get; set; }

        public BlockBasicInformationValidationProvider()
        {
            Logger = NullLogger.Instance;
        }

        public BlockForValidation ValidateBlockBeforeAppend(IBlock block)
        {
            Logger.Log("[BlockBasicInformationValidationProvider] Validating.\n");

            var blockForValidation = new BlockForValidation(block);

            if (block?.BlockHeader == null || block.BlockBody == null)
            {
                blockForValidation.ValidationMessage = "Null";
                return blockForValidation;
            }

            var blockHeader = block.BlockHeader;
            if (blockHeader.Height == 0 || blockHeader.PreviousBlockHash == null ||
                blockHeader.MerkleTreeRootHash == null)
            {
                blockForValidation.ValidationMessage = "Incorrect information.";
                return blockForValidation;
            }

            var blockBody = block.BlockBody;
            if (blockBody.Transactions == null)
            {
                blockForValidation.ValidationMessage = "Transactions is null.";
                return blockForValidation;
            }

            blockForValidation.Success = true;
            return blockForValidation;
        }
    }
}