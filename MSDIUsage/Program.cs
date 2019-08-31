using System;
using System.Linq;
using Demo.BlockChainServices;
using Demo.BlockEntities;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.MSDIUsage
{
    public static class Program
    {
        public static void Main()
        {
            // MS.DI中，DI容器的生成分为两步：
            // 先初始化ServiceCollection，进行一系列注册
            // 再通过ServiceCollection生成
            var serviceCollection = new ServiceCollection();

            // 配置依赖关系
            serviceCollection.AddSingleton<IBlockChainService, BlockChainService>();
            serviceCollection.AddSingleton<ITransactionPool, TransactionPool>();

            serviceCollection.AddTransient<IMineService, MineService>();
            serviceCollection.AddTransient<ITransactionExecutingService, TransactionExecutingService>();
            serviceCollection.AddTransient<INetworkService, NetworkService>();

            serviceCollection.AddTransient<ConsoleLoggerWithTimestamp>();
            serviceCollection.AddTransient<ILogger, ConsoleLoggerWithTimestampDecorator>();
            serviceCollection.AddTransient<IChainIdProvider, MSDIChainIdProvider>();
            serviceCollection.AddTransient<IBlockValidationService, BlockValidationService>();
            
            // 有新的验证逻辑的时候，添加一个IBlockValidationProvider的实现，在这里添加依赖关系就行了
            serviceCollection.AddSingleton<IBlockValidationProvider, BlockTransactionValidationProvider>();
            serviceCollection.AddSingleton<IBlockValidationProvider, BlockBasicInformationValidationProvider>();

            var container = serviceCollection.BuildServiceProvider();

            // 组合根结束

            // 配置Logger
            var loggingTypes = from type in typeof(IBlockChainService).Assembly.GetTypes()
                where !type.IsInterface
                where type.GetProperties().Any(p => p.PropertyType == typeof(ILogger))
                select type;
            var logger = container.GetRequiredService<ILogger>();
            foreach (var loggingType in loggingTypes.ToList())
            {
                var loggerProperty = loggingType.GetProperty("Logger");
                var targetInstance = container.GetServices(loggingType.GetInterfaces().First())
                    .First(i => i.GetType() == loggingType);
                loggerProperty?.SetValue(targetInstance, logger);
            }

            using (var scope = container.CreateScope())
            {
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
}