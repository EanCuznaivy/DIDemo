using Demo.BlockChainServices;
using Demo.BlockEntities;

namespace Demo.PureDIUsage
{
    public static class Program
    {
        // Composition Root
        // Pure DI
        public static void Main()
        {
            // 准备数据：链ID
            var chainId = Hash.FromString("DNT");

            // 出创世区块：通过执行一批交易来初始化整条链，一般包括部署系统合约，如共识合约，多资产合约等，并对系统合约做初步配置
            //   准备交易
            var transactionPool = new TransactionPool
            {
                Logger = new ConsoleLogger()
            };
            foreach (var genesisTransaction in TransactionGenerationHelper.GetGenesisTransactions())
            {
                transactionPool.AddTransaction(genesisTransaction);
            }

            //   打包创世区块
            //     准备MineService
            var blockChainService = new BlockChainService(chainId)
            {
                Logger = new ConsoleLogger()
            };
            var transactionExecutingService = new TransactionExecutingService
            {
                Logger = new ConsoleLogger()
            };
            
            var mineService = new MineService(transactionPool, blockChainService, transactionExecutingService);
            
            //     生产创世区块
            var genesisBlock = mineService.Mine();
            //     把创世区块添加到链上
            blockChainService.AppendBlock(genesisBlock);
            //   创世区块不广播，每一个节点都独立生成创世区块
            
            // 所以NetworkService实例较晚才出现
            var networkService = new NetworkService
            {
                Logger = new ConsoleLogger()
            };

            // 开始产生其他区块 - 挖矿
            // Demo: 出2个块
            var count = 2;
            while (count > 0)
            {
                // 交易应该是从网络收取的，这里随机生成一些
                var txs = TransactionGenerationHelper.GetSomeRandomTransactions();
                // 正常流程：从网络中收到交易，验证交易，然后丢进交易池
                foreach (var tx in txs)
                {
                    transactionPool.AddTransaction(tx);
                }
                // 交易池里已经有交易了，然后开始出块
                var block = mineService.Mine();
                // 拿到区块以后，自己做一些列验证，没问题的话就广播，并添加到本地的链上
                networkService.BroadcastBlock(block);
                blockChainService.AppendBlock(block);
                count--;
            }
            // 实际上，共识决定了谁应该在什么时候出块，以及收到一个区块以后该怎么验证这个区块的合法性
        }
    }
}