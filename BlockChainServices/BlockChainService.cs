using System.Collections.Generic;
using System.Linq;
using Demo.BlockEntities;

namespace Demo.BlockChainServices
{
    public class BlockChainService : IBlockChainService
    {
        public Hash ChainId { get; }
        private readonly List<IBlock> _blocks = new List<IBlock>();

        public ILogger Logger { get; set; }

        public BlockChainService(Hash chainId)
        {
            ChainId = chainId;

            // Null Object Pattern
            Logger = NullLogger.Instance;
        }

        public bool AppendBlock(IBlock block)
        {
            Logger.Log($"[BlockChainService] Appending block: \n{block}");
            if (!_blocks.Any())
            {
                _blocks.Add(block);
                return true;
            }
            var latestBlock = _blocks.Last();
            // 实际上区块验证应该提出去，以插件的形式
            if (latestBlock.GetHash().ToHex() == block.BlockHeader.PreviousBlockHash.ToHex() &&
                latestBlock.BlockHeader.Height + 1 == block.BlockHeader.Height)
            {
                _blocks.Add(block);
                return true;
            }
            
            Logger.Log("[BlockChainService] Appending failed.");

            return false;
        }

        public Hash GetLatestBlockHash()
        {
            return _blocks.LastOrDefault()?.GetHash() ?? Hash.Empty;
        }

        public long GetCurrentHeight()
        {
            return _blocks.LastOrDefault()?.BlockHeader.Height ?? 0;
        }
    }
}