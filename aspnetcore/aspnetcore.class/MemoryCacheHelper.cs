using System.Collections;
using Microsoft.Extensions.Caching.Memory;

namespace aspnetcore.cache;

public interface IMemoryCacheHelper
{
    TResult? GetOrCreate<TResult>(string cacheKey, Func<ICacheEntry, TResult?> valueFactory, int expireSeconds);
    Task<TResult?> GetOrCreateAsync<TResult>(string cacheKey, Func<ICacheEntry, Task<TResult?>> valueFactory, int expireSeconds);
    void Remove(string cacheKey);
}

public class MemoryCacheHelper : IMemoryCacheHelper
{
    private readonly IMemoryCache memoryCache;
    private readonly ICacheValueTypeValidator cacheValueTypeValidator;
    private readonly IExpirationRandom expirationRandom;

    public TResult? GetOrCreate<TResult>(string cacheKey, Func<ICacheEntry, TResult?> valueFactory, int expireSeconds = 60)
    {
        cacheValueTypeValidator.ValidateValueType<TResult>();
        return memoryCache.GetOrCreate(cacheKey, (e) =>
        {
            InitCacheEntry(e, expireSeconds);
            return valueFactory.Invoke(e);
        });
    }

    public async Task<TResult?> GetOrCreateAsync<TResult>(string cacheKey, Func<ICacheEntry, Task<TResult?>> valueFactory, int expireSeconds = 60)
    {
        cacheValueTypeValidator.ValidateValueType<TResult>();
        return await memoryCache.GetOrCreateAsync(cacheKey, async (e) =>
        {
            InitCacheEntry(e, expireSeconds);
            return await valueFactory.Invoke(e);
        });
    }

    public void Remove(string cacheKey)
    {
        memoryCache.Remove(cacheKey);
    }

    public MemoryCacheHelper(IMemoryCache memoryCache,
    IExpirationRandom expirationRandom,
    ICacheValueTypeValidator cacheValueTypeValidator)
    {
        this.expirationRandom = expirationRandom;
        this.memoryCache = memoryCache;
        this.cacheValueTypeValidator = cacheValueTypeValidator;
    }

    private void InitCacheEntry(ICacheEntry entry, int baseExpirationSeconds)
    {
        var expiration = expirationRandom.Next(baseExpirationSeconds);
        entry.AbsoluteExpirationRelativeToNow = expiration;
    }
}