using Microsoft.Extensions.DependencyInjection;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            // MS.DI中，DI容器的生成分为两步：
            // 先初始化ServiceCollection，进行一系列注册
            // 再通过ServiceCollection生成
            var serviceCollection = new ServiceCollection();
            
        }
    }
}