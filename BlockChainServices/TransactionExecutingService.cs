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
        public List<Hash> FillBlock(ref IBlock block, List<Transaction> transactions)
        {
            var executedTxIds = new List<Hash>();
            foreach (var transaction in transactions)
            {
                if (ExecuteTransaction(transaction))
                {
                    var txId = transaction.GetTransactionId();
                    block.BlockHeader.TransactionIds.Add(txId);
                    block.BlockBody.Transactions.Add(transaction);
                    executedTxIds.Add(txId);
                    Logger.Log($"[TxExecutingService] Executed transaction: {transaction}");
                }
            }

            return executedTxIds;
        }

        // TODO: 执行过程
        private bool ExecuteTransaction(Transaction transaction)
        {
            return true;
        }
    }
}