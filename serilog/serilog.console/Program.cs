using Serilog;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Formatting.Json;
using Exceptionless;

var services = new ServiceCollection();
services.AddScoped<Test1>();
services.AddLogging(loggingBuilder =>
{
    ExceptionlessClient.Default.Startup("feMf2z4Sb6l88bbuceqJTduiBifNQcqvfmsMZNlO");
    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .Enrich.FromLogContext()
                        .WriteTo.Console(new JsonFormatter())
                        .WriteTo.Exceptionless()
                        .CreateLogger();
    loggingBuilder.AddSerilog(dispose: true);
});


using (var sp = services.BuildServiceProvider())
{
    var test1 = sp.GetRequiredService<Test1>();
    test1.Test();
}