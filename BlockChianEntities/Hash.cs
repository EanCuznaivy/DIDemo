using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Demo.BlockEntities
{
    public class Hash : IComparable<Hash>
    {
        public static readonly Hash Empty = FromByteArray(Enumerable.Range(0, Constants.HashByteArrayLength)
            .Select(x => byte.MinValue).ToArray());

        // 不诚实
        public bool IsValid { get; }

        public byte[] Value { get; }

        private Hash(byte[] value)
        {
            Value = value;
            if (value.Length != Constants.HashByteArrayLength)
            {
                IsValid = false;
            }
        }

        public static Hash FromRawBytes(byte[] bytes)
        {
            return new Hash(SHA256.Create().ComputeHash(bytes));
        }

        public static Hash FromByteArray(byte[] bytes)
        {
            return new Hash(bytes);
        }

        public static Hash FromString(string str)
        {
            return FromRawBytes(Encoding.UTF8.GetBytes(str));
        }

        public static Hash FromTwoHashes(Hash hash1, Hash hash2)
        {
            var hashes = new List<Hash>
            {
                hash1, hash2
            };
            using (var mm = new MemoryStream())
            {
                var bytes = new byte[] { };
                foreach (var hash in hashes.OrderBy(x => x))
                {
                    bytes = (byte[]) bytes.Concat(hash.Value);
                }

                mm.Flush();
                return FromRawBytes(bytes);
            }
        }

        public static Hash Generate()
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

        public static bool operator ==(Hash h1, Hash h2)
        {
            return h1?.Equals(h2) ?? ReferenceEquals(h2, null);
        }

        public static bool operator !=(Hash h1, Hash h2)
        {
            return !(h1 == h2);
        }

        public static bool operator <(Hash h1, Hash h2)
        {
            return CompareHash(h1, h2) < 0;
        }

        public static bool operator >(Hash h1, Hash h2)
        {
            return CompareHash(h1, h2) > 0;
        }

        private static int CompareHash(Hash hash1, Hash hash2)
        {
            if (hash1 != null)
            {
                return hash2 == null ? 1 : Compare(hash1.Value, hash2.Value);
            }

            if (hash2 == null)
            {
                return 0;
            }

            return -1;
        }

        private static int Compare(IReadOnlyList<byte> xValue, IReadOnlyList<byte> yValue)
        {
            for (var i = 0; i < Math.Min(xValue.Count, yValue.Count); i++)
            {
                if (xValue[i] > yValue[i])
                {
                    return 1;
                }

                if (xValue[i] < yValue[i])
                {
                    return -1;
                }
            }

            return 0;
        }

        private bool Equals(Hash other)
        {
            return IsValid == other.IsValid && Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Hash) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (IsValid.GetHashCode() * 397) ^ (Value != null ? Value.GetHashCode() : 0);
            }
        }

        public int CompareTo(Hash that)
        {
            if (that == null)
                throw new InvalidOperationException("Cannot compare hash when hash is null");

            return CompareHash(this, that);
        }
    }
}