using Demo.BlockChainServices;
using Demo.BlockEntities;

namespace Demo.PureDIUsage
{
    public class PureDIChainIdProvider : IChainIdProvider
    {
        public Hash ChainId => Hash.FromString("PURE");
    }
}