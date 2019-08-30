using System;
using System.Linq;
using System.Security.Cryptography;

namespace Demo.BlockEntities
{
    // TODO: 应该由public key生成
    public class Address
    {
        public static readonly Address Zero = FromByteArray(Enumerable.Range(0, Constants.HashByteArrayLength)
            .Select(x => byte.MinValue).ToArray());

        public byte[] Value { get; set; }

        private Address(byte[] value)
        {
            Value = value;
            if (value.Length != Constants.AddressByteArrayLength)
            {
                throw new InvalidOperationException("Incorrect length.");
            }
        }

        public static Address FromRawBytes(byte[] bytes)
        {
            return new Address(SHA256.Create().ComputeHash(bytes));
        }

        public static Address FromByteArray(byte[] bytes)
        {
            return new Address(bytes);
        }

        public static Address Generate()
        {
            return FromRawBytes(Guid.NewGuid().ToByteArray());
        }
    }
}