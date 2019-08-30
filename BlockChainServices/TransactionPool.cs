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
        public List<Hash> TransactionIds => _transactions.Select(t => t.GetTransactionId()).ToList();

        public void AddTransaction(Transaction transaction)
        {
            if (!TransactionIds.Contains(transaction.GetTransactionId()))
            {
                _transactions.Add(transaction);
            }
        }

        public Transaction GetTransactionById(Hash transactionId)
        {
            return TransactionIds.Contains(transactionId)
                ? _transactions.First(t => t.GetTransactionId() == transactionId)
                : null;
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return _transactions;
        }

        public void RemoveTransactions(IEnumerable<Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                _transactions.Remove(transaction);
            }
        }
    }
}