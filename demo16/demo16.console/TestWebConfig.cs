using Microsoft.Extensions.Options;

class TestWebConfig
{
    private IOptionsSnapshot<WebConifg> optWebConfig;
    public TestWebConfig(IOptionsSnapshot<WebConifg> optWebConfig)
    {
        this.optWebConfig = optWebConfig;
    }

    public void Test()
    {
        var value = optWebConfig.Value;
        Console.WriteLine(value);
    }
}