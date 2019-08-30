using System.Collections.Generic;

namespace Demo.BlockEntities
{
    public interface IBlockBody
    {
        List<Transaction> Transactions { get; set; }
    }
}