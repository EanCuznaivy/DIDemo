using System.Collections.Generic;

namespace Demo.BlockEntities
{
    public interface IBlockHeader
    {
        long Height { get; }
        Hash PreviousBlockHash { get; }
        List<Hash> TransactionIds { get; set; }
        Hash MerkleTreeRootHash { get; set; }

        Hash GetHash();
    }
}