namespace Demo.BlockEntities
{
    // 其实这个接口实用意义不大，主要是为了演示装饰器模式和
    // [ISP]
    public interface IValidationMessageProvider
    {
        bool Success { get; }
        string ValidationMessage { get; }
    }
}