namespace Demo.BlockEntities
{
    public interface IBlock
    {
        IBlockHeader BlockHeader { get; }
        IBlockBody BlockBody { get; }

        Hash GetHash();
    }
}