namespace Demo.BlockEntities
{
    public interface IHashComputer
    {
        Hash ComputeHashFromBytes(byte[] bytes);
    }
}