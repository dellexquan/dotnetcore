using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var services = new ServiceCollection();
services.AddScoped<Test1>();
services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.SetMinimumLevel(LogLevel.Debug);
});

using (var sp = services.BuildServiceProvider())
{
    var test1 = sp.GetRequiredService<Test1>();
    test1.Test();
}

// System.Console.WriteLine("DI complete!");