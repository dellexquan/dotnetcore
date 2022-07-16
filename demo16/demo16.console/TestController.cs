using Microsoft.Extensions.Options;

class TestController
{
    private readonly IOptionsSnapshot<Config> optConfig;
    private readonly IOptionsSnapshot<Proxy> optProxy;

    public TestController(IOptionsSnapshot<Config> optConfig, IOptionsSnapshot<Proxy> optProxy)
    {
        this.optConfig = optConfig;
        this.optProxy = optProxy;
    }

    public void Test()
    {
        // System.Console.WriteLine(optConfig.Value.Age);
        // System.Console.WriteLine("************");
        // System.Console.WriteLine(optConfig.Value.Age);
        System.Console.WriteLine(optConfig.Value);
        System.Console.WriteLine(optProxy.Value);
    }
}