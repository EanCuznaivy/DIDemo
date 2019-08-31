using System.Collections.Generic;
using System.Linq;
using Demo.BlockEntities;

namespace Demo.BlockChainServices
{
    public class BlockChainService : IBlockChainService
    {
        private readonly IBlockValidationService _blockValidationService;
        public Hash ChainId { get; }
        private readonly List<IBlock> _blocks = new List<IBlock>();

        public ILogger Logger { get; set; }

        // 这里如果直接传入ChainId，会发现构造器受限，这个依赖太具体了，改成用IChainIdProvider会好一些
        // 组合根可以决定用哪个IChainIdProvider的实现
        // 用IChainIdProvider的最大的好处是可以直接为BlockChainService注入别的依赖而不用再次修改组合根的代码（使用DI容器的时候）
        // 没理解可以看看提交历史
        public BlockChainService(IChainIdProvider chainIdProvider, IBlockValidationService blockValidationService)
        {
            _blockValidationService = blockValidationService;
            ChainId = chainIdProvider.ChainId;

            // Null Object Pattern
            Logger = NullLogger.Instance;
        }

        public bool AppendBlock(IBlock block)
        {
            Logger.Log($"[BlockChainService] Appending block: \n{block}");
            if (!_blockValidationService.ValidateBlockBeforeAppend(block))
            {
                Logger.Log($"[BlockChainService] Block failed to pass validation.");
                return false;
            }

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