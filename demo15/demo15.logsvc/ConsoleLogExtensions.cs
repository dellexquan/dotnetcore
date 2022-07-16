using demo15.logsvc;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConsoleLogExtensions
{
    public static void AddConsoleLog(this IServiceCollection services)
    {
        services.AddScoped<ILogProvider, ConsoleLogProvider>();
    }
}
