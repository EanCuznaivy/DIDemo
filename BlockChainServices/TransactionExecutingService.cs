using System.Collections.Generic;
using Demo.BlockEntities;

namespace Demo.BlockChainServices
{
    public class TransactionExecutingService : ITransactionExecutingService
    {
        public ILogger Logger { get; set; }

        public TransactionExecutingService()
        {
            Logger = NullLogger.Instance;
        }

        // TODO: 控制执行交易的总时间，或者每一次执行的交易数量
        public void FillBlock(ref IBlock block, IEnumerable<Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                if (ExecuteTransaction(transaction))
                {
                    block.BlockHeader.TransactionIds.Add(transaction.GetTransactionId());
                    block.BlockBody.Transactions.Add(transaction);
                }
            }
        }

        // TODO: 执行过程
        private bool ExecuteTransaction(Transaction transaction)
        {
            return true;
        }
    }
}