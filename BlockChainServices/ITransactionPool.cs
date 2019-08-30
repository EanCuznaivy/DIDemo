using System.Collections.Generic;
using Demo.BlockEntities;

namespace Demo.BlockChainServices
{
    public interface ITransactionPool
    {
        void AddTransaction(Transaction transaction);
        Transaction GetTransactionById(Hash transactionId);
        IEnumerable<Transaction> GetAllTransactions();
        void RemoveTransactions(IEnumerable<Transaction> transactions);
    }
}