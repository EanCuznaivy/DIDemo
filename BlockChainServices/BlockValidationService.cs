using System.Collections.Generic;
using System.Linq;
using Demo.BlockEntities;

namespace Demo.BlockChainServices
{
    public class BlockValidationService : IBlockValidationService
    {
        public ILogger Logger { get; set; }
        private readonly IEnumerable<IBlockValidationProvider> _blockValidationProviders;

        public BlockValidationService(IEnumerable<IBlockValidationProvider> blockValidationProviders)
        {
            _blockValidationProviders = blockValidationProviders;
            Logger = NullLogger.Instance;
        }

        public bool ValidateBlockBeforeAppend(IBlock block)
        {
            return _blockValidationProviders.All(provider => provider.ValidateBlockBeforeAppend(block));
        }
    }
}