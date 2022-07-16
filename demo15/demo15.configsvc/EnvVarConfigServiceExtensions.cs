using demo15.configsvc;

namespace Microsoft.Extensions.DependencyInjection;

public static class EnvVarConfigServiceExtensions
{
    public static void AddEnvVarConfigService(this IServiceCollection services)
    {
        services.AddScoped<IConfigService, EnvVarConfigService>();
    }
}