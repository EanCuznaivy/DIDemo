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
            var validatedBlocks = _blockValidationProviders
                .Select(provider => provider.ValidateBlockBeforeAppend(block)).ToList();

            if (validatedBlocks.All(b => b.Success))
            {
                return true;
            }

            validatedBlocks.Where(b => !b.Success).ToList().ForEach(b => Logger.Log($"{b}\n: {b.ValidationMessage}"));

            return false;
        }
    }
}