namespace Demo.BlockEntities
{
    // [LSP]
    public interface IBlock
    {
        IBlockHeader BlockHeader { get; }
        IBlockBody BlockBody { get; }

        Hash GetHash();
    }
}