using System;
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

        public bool ValidateBlockBeforeAppend(IBlock block)
        {
            Logger.Log("[BlockBasicInformationValidationProvider] Validating.\n");

            if (block == null || block.BlockHeader == null || block.BlockBody == null)
            {
                return false;
            }

            var blockHeader = block.BlockHeader;
            if (blockHeader.Height == 0 || blockHeader.PreviousBlockHash == null ||
                blockHeader.MerkleTreeRootHash == null)
            {
                return false;
            }

            var blockBody = block.BlockBody;
            if (blockBody.Transactions == null)
            {
                return false;
            }

            return true;
        }
    }
}