using System;

namespace Demo.BlockEntities
{
    public class TransactionForValidation : ITransaction, IValidationMessageProvider
    {
        private readonly ITransaction _transaction;
        public Address From => _transaction.From;
        public Address To => _transaction.To;
        public DateTimeOffset Timestamp => _transaction.Timestamp;
        public string MethodName => _transaction.MethodName;
        public byte[] Params => _transaction.Params;

        public TransactionForValidation(ITransaction transaction)
        {
            _transaction = transaction;
        }

        public Hash GetTransactionId()
        {
            return _transaction.GetTransactionId();
        }

        public bool Success { get; set; }
        public string ValidationMessage { get; set; }

        public override string ToString()
        {
            return _transaction.ToString();
        }
    }
}