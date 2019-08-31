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

        public bool ValidateBlockBeforeAppend(IBlock block)
        {
            Logger.Log("[BlockTransactionValidationProvider] Validating.\n");

            var txCount = block.BlockHeader.TransactionIds.Count;
            if (txCount == 0)
            {
                return false;
            }

            if (block.BlockBody.Transactions.Count != txCount)
            {
                return false;
            }

            var txIdsInHeader = block.BlockHeader.TransactionIds.Select(tx => tx.ToHex());
            var txIdsInBody = block.BlockBody.Transactions.Select(tx => tx.GetTransactionId().ToHex());
            if (txIdsInHeader.Intersect(txIdsInBody).Count() != txCount)
            {
                return false;
            }

            return true;
        }
    }
}