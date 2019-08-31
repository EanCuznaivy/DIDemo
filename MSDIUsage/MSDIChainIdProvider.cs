using Demo.BlockChainServices;
using Demo.BlockEntities;

namespace Demo.MSDIUsage
{
    public class MSDIChainIdProvider : IChainIdProvider
    {
        public Hash ChainId => Hash.FromString("MSDI");
    }
}