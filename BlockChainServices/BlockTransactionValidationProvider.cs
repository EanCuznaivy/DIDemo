using System.Linq;
using Demo.BlockEntities;

namespace Demo.BlockChainServices
{
    public class BlockTransactionValidationProvider : IBlockValidationProvider
    {
        public ILogger Logger { get; set; }

        public BlockTransactionValidationProvider()
        {
            Logger = NullLogger.Instance;
        }

        public BlockForValidation ValidateBlockBeforeAppend(IBlock block)
        {
            Logger.Log("[BlockTransactionValidationProvider] Validating.\n");

            var blockForValidation = new BlockForValidation(block);

            var txCount = block.BlockHeader.TransactionIds.Count;
            if (txCount == 0)
            {
                blockForValidation.ValidationMessage = "No tx.";
                return blockForValidation;
            }

            if (block.BlockBody.Transactions.Count != txCount)
            {
                blockForValidation.ValidationMessage = "Tx count not match.";
                return blockForValidation;
            }

            var txIdsInHeader = block.BlockHeader.TransactionIds.Select(tx => tx.ToHex());
            var txIdsInBody = block.BlockBody.Transactions.Select(tx => tx.GetTransactionId().ToHex());
            if (txIdsInHeader.Intersect(txIdsInBody).Count() != txCount)
            {
                blockForValidation.ValidationMessage = "Incorrect txs.";
            }

            blockForValidation.Success = true;
            return blockForValidation;
        }
    }
}