using System.Collections.Generic;

namespace Demo.BlockEntities
{
    public class GenesisBlockHeader : BlockHeader
    {
        public GenesisBlockHeader(long height = 1, Hash previousBlockHash = null) : base(1, Hash.Empty)
        {
        }
    }
}