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
            get => Hash.FromRawBytes(TransactionIds.First().Value.Concat(TransactionIds.Last().Value).ToArray());
            set { }
        }

        public Hash GetHash()
        {
            return Hash.FromRawBytes(PreviousBlockHash.Value.Concat(MerkleTreeRootHash.Value).ToArray());
        }

        public override string ToString()
        {
            var txIds = TransactionIds.Aggregate("", (current, id) => current + id.ToHex() + "\n\t\t");
            return
                $"{{\n\tBlockHash: {GetHash()}\n\tHeight: {Height}\n\tPreviousHash: {PreviousBlockHash}\n\tTransactionIds:\n\t\t{txIds}\n}}";
        }
    }
}