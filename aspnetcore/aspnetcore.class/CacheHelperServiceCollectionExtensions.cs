using Microsoft.Extensions.DependencyInjection;

namespace aspnetcore.cache;

public static class CacheHelperServiceCollectionExtensions
{
    public static void AddCacheHelper(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IExpirationRandom, ExpirationRandom>();
        serviceCollection.AddScoped<ICacheValueTypeValidator, CacheValueTypeValidator>();
        serviceCollection.AddScoped<IMemoryCacheHelper, MemoryCacheHelper>();
        serviceCollection.AddScoped<IDistributedCacheHelper, DistributedCacheHelper>();
    }
}