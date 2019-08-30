using System;
using System.Collections.Generic;

namespace Demo.BlockEntities
{
    public class TransactionGenerationHelper
    {
        public static List<Transaction> GetGenesisTransactions()
        {
            return new List<Transaction>
            {
                new Transaction
                {
                    From = Address.Zero,
                    To = Address.Zero,
                    MethodName = "DeploySystemSmartContract",
                    Params = new byte[] {1}, // 假装这是共识合约的数据
                    Timestamp = DateTimeOffset.UtcNow
                },
                new Transaction
                {
                    From = Address.Zero,
                    To = Address.Zero,
                    MethodName = "DeploySystemSmartContract",
                    Params = new byte[] {2}, // 假装这是多资产合约的数据
                    Timestamp = DateTimeOffset.UtcNow
                }
            };
        }

        public static List<Transaction> GetSomeRandomTransactions()
        {
            return new List<Transaction>
            {
                new Transaction
                {
                    From = Address.Generate(),
                    To = Address.Generate(),
                    MethodName = "AAA",
                    Params = new byte[] {1},
                    Timestamp = DateTimeOffset.UtcNow
                },
                new Transaction
                {
                    From = Address.Generate(),
                    To = Address.Generate(),
                    MethodName = "BBB",
                    Params = new byte[] {2},
                    Timestamp = DateTimeOffset.UtcNow
                },
                new Transaction
                {
                    From = Address.Generate(),
                    To = Address.Generate(),
                    MethodName = "CCC",
                    Params = new byte[] {3},
                    Timestamp = DateTimeOffset.UtcNow
                },
            };
        }
    }
}