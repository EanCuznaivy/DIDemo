using System.Threading;
using Demo.BlockEntities;

namespace Demo.BlockChainServices
{
    public class MineService : IMineService
    {
        public ILogger Logger { get; set; }

        private readonly ITransactionPool _transactionPool;
        private readonly IBlockChainService _blockChainService;
        private readonly ITransactionExecutingService _transactionExecutingService;

        public MineService(ITransactionPool transactionPool, IBlockChainService blockChainService,
            ITransactionExecutingService transactionExecutingService)
        {
            _transactionPool = transactionPool;
            _blockChainService = blockChainService;
            _transactionExecutingService = transactionExecutingService;

            Logger = NullLogger.Instance;
        }

        public IBlock Mine()
        {
            Logger.Log("[MineService] Start mining.");
            // Generate a block template.
            IBlock block;
            var currentHeight = _blockChainService.GetCurrentHeight();
            if (currentHeight == 0)
            {
                block = new GenesisBlock(new GenesisBlockHeader(), new GenesisBlockBody());
            }
            else
            {
                var latestBlockHash = _blockChainService.GetLatestBlockHash();
                block = new Block(new BlockHeader(currentHeight + 1, latestBlockHash), new BlockBody());
            }

            // Fetch Transactions from tx pool.
            var txs = _transactionPool.GetAllTransactions();

            // Execute txs and fill the block.
            var executedTxIds = _transactionExecutingService.FillBlock(ref block, txs);
            
            // Remove executed txs.
            _transactionPool.RemoveTransactions(executedTxIds);

            Logger.Log($"[MineService] Mined a block: \n{block}");

            Thread.Sleep(1000);
            return block;
        }
    }
}