using Microsoft.Extensions.Configuration;
public static class ConfigurationBuilderExtensions
{
    // public IConfigurationBuilder Add(IConfigurationSource source);
    public static IConfigurationBuilder AddWebConfig(this IConfigurationBuilder builder, string path = "web.config")
    {
        return builder.Add(new FxConfigSource() { Path = path });
    }
}