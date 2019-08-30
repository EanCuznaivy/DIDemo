namespace Demo.BlockEntities
{
    public class GenesisBlock : IBlock
    {
        public IBlockHeader BlockHeader { get; }
        public IBlockBody BlockBody { get; }

        public GenesisBlock(IBlockHeader blockHeader, IBlockBody blockBody)
        {
            BlockHeader = blockHeader;
            BlockBody = blockBody;
        }

        public Hash GetHash() => BlockHeader.GetHash();
        
        public override string ToString() => $"The Genesis Block.\nBlockHeader: {BlockHeader}\nBlockBody: {BlockBody}";
    }
}