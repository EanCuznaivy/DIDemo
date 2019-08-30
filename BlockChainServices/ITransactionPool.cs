using System.Collections.Generic;
using Demo.BlockEntities;

namespace Demo.BlockChainServices
{
    public interface ITransactionPool
    {
        void AddTransaction(Transaction transaction);
        Transaction GetTransactionById(Hash transactionId);
        List<Transaction> GetAllTransactions();
        void RemoveTransactions(List<Hash> txIds);
    }
}