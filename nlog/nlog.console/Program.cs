using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;

var services = new ServiceCollection();
services.AddScoped<Test1>();
services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddNLog();
});


using (var sp = services.BuildServiceProvider())
{
    var test1 = sp.GetRequiredService<Test1>();
    test1.Test();
}