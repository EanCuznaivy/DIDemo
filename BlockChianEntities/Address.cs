using System;
using System.Linq;
using System.Security.Cryptography;

namespace Demo.BlockEntities
{
    // TODO: 应该通过非对称密钥对中的public key生成，而非自己填二进制数组
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

        public string ToHex(bool withPrefix = false)
        {
            var offset = withPrefix ? 2 : 0;
            var length = Value.Length * 2 + offset;
            var c = new char[length];

            if (withPrefix)
            {
                c[0] = '0';
                c[1] = 'x';
            }

            for (int bx = 0, cx = offset; bx < Value.Length; ++bx, ++cx)
            {
                var b = (byte) (Value[bx] >> 4);
                c[cx] = (char) (b > 9 ? b + 0x37 + 0x20 : b + 0x30);

                b = (byte) (Value[bx] & 0x0F);
                c[++cx] = (char) (b > 9 ? b + 0x37 + 0x20 : b + 0x30);
            }

            return new string(c);
        }

        public override string ToString()
        {
            return ToHex();
        }
    }
}