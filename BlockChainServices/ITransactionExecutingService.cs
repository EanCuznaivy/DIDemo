using System.Collections.Generic;
using Demo.BlockEntities;

namespace Demo.BlockChainServices
{
    public interface ITransactionExecutingService
    {
        List<Hash> FillBlock(ref IBlock block, List<Transaction> transactions);
    }
}