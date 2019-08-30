using System;
using System.Linq;

namespace Demo.BlockEntities
{
    public class Transaction
    {
        public Address From { get; set; }
        public Address To { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string MethodName { get; set; }
        public byte[] Params { get; set; }

        public Hash GetTransactionId()
        {
            // 按理说应该序列化交易本身，得到Id
            return Hash.FromRawBytes(From.Value.Concat(To.Value).Concat(Params).ToArray());
        }

        public override string ToString()
        {
            return
                $"{{\n\tTxId: {GetTransactionId()}\n\tFrom: {From}\n\tTo: {To}\n\tTimestamp: {Timestamp}\n\tMethodName: {MethodName}\n}}\n";
        }
    }
}