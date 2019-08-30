using System.Collections.Generic;
using System.Linq;

namespace Demo.BlockEntities
{
    public class BlockBody : IBlockBody
    {
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public override string ToString()
        {
            return Transactions.Aggregate("", (current, tx) => current + tx + "\n");
        }
    }
}