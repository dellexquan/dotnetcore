using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;
using SystemServices;

var services = new ServiceCollection();
services.AddScoped<Test1>();
services.AddScoped<Test2>();
services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddNLog();
});


using (var sp = services.BuildServiceProvider())
{
    var test1 = sp.GetRequiredService<Test1>();
    test1.Test();
    var test2 = sp.GetRequiredService<Test2>();
    test2.Test();
}