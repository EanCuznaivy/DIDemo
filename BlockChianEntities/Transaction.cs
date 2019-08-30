using System;

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
            return Hash.Generate();
        }

        public override string ToString()
        {
            return $"\nFrom: {From}\nTo: {To}\nTimestamp: {Timestamp}\nMethodName: {MethodName}";
        }
    }
}