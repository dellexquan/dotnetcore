using demo15.configsvc;

namespace Microsoft.Extensions.DependencyInjection;

public static class IniFileConfigServiceExtensions
{
    public static void AddIniFileConfigService(this IServiceCollection services, string fileName)
    {
        services.AddScoped(typeof(IConfigService), s => new IniFileConfigService(fileName));
    }
}