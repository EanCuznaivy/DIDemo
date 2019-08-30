using System.Linq;
using Demo.BlockChainServices;
using Demo.BlockEntities;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.MSDIUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            // MS.DI中，DI容器的生成分为两步：
            // 先初始化ServiceCollection，进行一系列注册
            // 再通过ServiceCollection生成
            var serviceCollection = new ServiceCollection();

            // 配置依赖关系
            serviceCollection.AddSingleton<IBlockChainService>(new BlockChainService(Hash.FromString("DHT")));

            serviceCollection.AddSingleton<ITransactionPool, TransactionPool>();
            serviceCollection.AddSingleton<IMineService, MineService>();
            serviceCollection.AddSingleton<ITransactionExecutingService, TransactionExecutingService>();
            serviceCollection.AddSingleton<INetworkService, NetworkService>();

            serviceCollection.AddTransient<ILogger, ConsoleLogger>();

            var container = serviceCollection.BuildServiceProvider(true);

            // 配置Logger
            var loggingTypes = from type in typeof(IBlockChainService).Assembly.GetTypes()
                where !type.IsInterface
                where type.GetProperties().Any(p => p.PropertyType == typeof(ILogger))
                select type;
            foreach (var loggingType in loggingTypes.ToList())
            {
                var loggerProperty = loggingType.GetProperties().Single(p => p.PropertyType == typeof(ILogger));
                loggerProperty.SetValue(container.GetRequiredService(loggingType.GetInterfaces().First()),
                    container.GetRequiredService<ILogger>());
            }

            var scope = container.CreateScope();

            // 从scope中根据依赖获取实例
            var serviceProvider = scope.ServiceProvider;
            var transactionPool = serviceProvider.GetRequiredService<ITransactionPool>();
            var blockChainService = serviceProvider.GetRequiredService<IBlockChainService>();
            var mineService = serviceProvider.GetRequiredService<IMineService>();
            var transactionExecutingService =
                serviceProvider.GetRequiredService<ITransactionExecutingService>(); // 这里没有用
            var networkService = serviceProvider.GetRequiredService<INetworkService>();

            // 开始了

            // 准备创世交易
            foreach (var genesisTx in TransactionGenerationHelper.GetGenesisTransactions())
            {
                transactionPool.AddTransaction(genesisTx);
            }

            // 打包添加创世区块
            var genesisBlock = mineService.Mine();
            blockChainService.AppendBlock(genesisBlock);
            // 打包其他区块
            var count = 10;
            while (count > 0)
            {
                var txs = TransactionGenerationHelper.GetSomeRandomTransactions();
                foreach (var tx in txs)
                {
                    transactionPool.AddTransaction(tx);
                }

                var block = mineService.Mine();
                blockChainService.AppendBlock(block);
                networkService.BroadcastBlock(block);
                count--;
            }
        }
    }
}