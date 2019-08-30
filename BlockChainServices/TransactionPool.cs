using System.Collections.Generic;
using System.Linq;
using Demo.BlockEntities;

namespace Demo.BlockChainServices
{
    public class TransactionPool : ITransactionPool
    {
        public ILogger Logger { get; set; }

        public TransactionPool()
        {
            Logger = NullLogger.Instance;
        }

        private readonly List<Transaction> _transactions = new List<Transaction>();
        public List<string> TransactionIds => _transactions.Select(t => t.GetTransactionId().ToHex()).ToList();

        public void AddTransaction(Transaction transaction)
        {
            // 实际上交易在进入交易池之前也要过一遍验证，这里只阻止了交易重复进入
            if (!TransactionIds.Contains(transaction.GetTransactionId().ToHex()))
            {
                _transactions.Add(transaction);
                Logger.Log($"[TxPool] Added transaction {transaction} to pool.");
            }
        }

        public Transaction GetTransactionById(Hash transactionId)
        {
            return TransactionIds.Contains(transactionId.ToHex())
                ? _transactions.First(t => t.GetTransactionId().ToHex() == transactionId.ToHex())
                : null;
        }

        public List<Transaction> GetAllTransactions()
        {
            return _transactions;
        }

        public void RemoveTransactions(List<Hash> txIds)
        {
            foreach (var id in txIds)
            {
                var tx = _transactions.First(t => t.GetTransactionId().ToHex() == id.ToHex());
                _transactions.Remove(tx);
                Logger.Log($"[TxPool] Removed transaction {tx} from pool.");
            }
        }
    }
}