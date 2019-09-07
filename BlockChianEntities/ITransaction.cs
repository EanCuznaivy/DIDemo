using System;

namespace Demo.BlockEntities
{
    public interface ITransaction
    {
        Address From { get; }
        Address To { get; }
        DateTimeOffset Timestamp { get; }
        string MethodName { get; }
        byte[] Params { get; }

        Hash GetTransactionId();
    }
}