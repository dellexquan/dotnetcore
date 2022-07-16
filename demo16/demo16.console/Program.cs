using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



var configBuilder = new ConfigurationBuilder();
// configBuilder.Add(new FxConfigSource() { Path = "web.config" });
// configBuilder.AddWebConfig("web.config");
configBuilder.AddJsonFile("config.json");
configBuilder.AddCommandLine(args);
configBuilder.AddEnvironmentVariables("a1_");
var config = configBuilder.Build();

// System.Console.WriteLine(args.Length);
// System.Console.WriteLine(args[0]);

var services = new ServiceCollection();
services.AddOptions()
        .Configure<Config>(e => config.Bind(e))
        .Configure<Proxy>(e => config.GetSection("proxy").Bind(e));
services.AddScoped<TestController>();
// services.AddOptions().Configure<WebConifg>(e => config.Bind(e));
// services.AddScoped<TestWebConfig>();

// var name = config["name"];
// var proxyAddress = config.GetSection("proxy:address").Value;
// Console.WriteLine(name);
// Console.WriteLine(proxyAddress);

// var proxy = config.GetSection("proxy").Get<Proxy>();
// System.Console.WriteLine(proxy);

// var cfg = config.Get<Config>();
// System.Console.WriteLine(cfg);

using (var sp = services.BuildServiceProvider())
{
    var ctrl = sp.GetRequiredService<TestController>();
    ctrl.Test();
    // var cfg = sp.GetRequiredService<TestWebConfig>();
    // cfg.Test();
}


record Config
{
    public string? Name { get; set; }
    public int Age { get; set; }
    public Proxy? Proxy { get; set; }
}
record Proxy
{
    public string? Address { get; set; }
    public int Port { get; set; }
}