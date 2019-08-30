using System.Collections.Generic;
using Demo.BlockEntities;

namespace Demo.BlockChainServices
{
    public interface ITransactionExecutingService
    {
        void FillBlock(ref IBlock block, IEnumerable<Transaction> transactions);
    }
}