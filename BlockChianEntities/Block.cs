namespace Demo.BlockEntities
{
    public class Block : IBlock
    {
        public IBlockHeader BlockHeader { get; }
        public IBlockBody BlockBody { get; }

        public Block(IBlockHeader blockHeader, IBlockBody blockBody)
        {
            BlockHeader = blockHeader;
            BlockBody = blockBody;
        }

        public Hash GetHash() => BlockHeader.GetHash();

        public override string ToString() =>
            $"A Normal Block of Height {BlockHeader.Height}.\nBlockHeader: {BlockHeader}\nBlockBody: {BlockBody}";
    }
}