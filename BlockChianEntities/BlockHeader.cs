using System.Collections.Generic;
using System.Linq;

namespace Demo.BlockEntities
{
    public class BlockHeader : IBlockHeader
    {
        public BlockHeader(long height, Hash previousBlockHash)
        {
            Height = height;
            PreviousBlockHash = previousBlockHash;
        }

        public long Height { get; }
        public Hash PreviousBlockHash { get; }
        public List<Hash> TransactionIds { get; set; } = new List<Hash>();

        public Hash MerkleTreeRootHash
        {
            get => Hash.Generate();
            set { }
        }

        public Hash GetHash()
        {
            return Hash.Generate();
        }

        public override string ToString()
        {
            var txIds = TransactionIds.Aggregate("", (current, id) => current + id.ToHex() + "\n");
            return $"\nHeight: {Height}\nPreviousHash: {PreviousBlockHash}\nTransactionIds:{txIds}";
        }
    }
}