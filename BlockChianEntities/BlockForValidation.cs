namespace Demo.BlockEntities
{
    public class BlockForValidation : IBlock, IValidationMessageProvider
    {
        private readonly IBlock _block;

        public BlockForValidation(IBlock block)
        {
            _block = block;
        }

        public IBlockHeader BlockHeader => _block.BlockHeader;
        public IBlockBody BlockBody => _block.BlockBody;

        public Hash GetHash()
        {
            return _block.GetHash();
        }

        public bool Success { get; set; }
        public string ValidationMessage { get; set; }

        public override string ToString()
        {
            return _block.ToString();
        }
    }
}