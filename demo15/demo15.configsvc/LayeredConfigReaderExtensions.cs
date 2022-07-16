using demo15.configsvc;

namespace Microsoft.Extensions.DependencyInjection;

public static class LayeredConfigReaderExtensions
{
    public static void AddLayeredConfig(this ServiceCollection services)
    {
        services.AddScoped<IConfigReader, LayeredConfigReader>();
    }
}