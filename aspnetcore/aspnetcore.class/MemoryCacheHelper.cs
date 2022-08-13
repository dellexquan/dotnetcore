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

    public TResult? GetOrCreate<TResult>(string cacheKey, Func<ICacheEntry, TResult?> valueFactory, int expireSeconds = 60)
    {
        validateValueType<TResult>();
        return memoryCache.GetOrCreate(cacheKey, (e) =>
        {
            InitCacheEntry(e, expireSeconds);
            return valueFactory.Invoke(e);
        });
    }

    public async Task<TResult?> GetOrCreateAsync<TResult>(string cacheKey, Func<ICacheEntry, Task<TResult?>> valueFactory, int expireSeconds = 60)
    {
        validateValueType<TResult>();
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

    public MemoryCacheHelper(IMemoryCache memoryCache)
    {
        this.memoryCache = memoryCache;
    }

    private static void validateValueType<TResult>()
    {
        var typeResult = typeof(TResult);
        if (typeResult.IsGenericType)
        {
            typeResult = typeResult.GetGenericTypeDefinition();
        }
        if (typeResult == typeof(IEnumerable)
        || typeResult == typeof(IEnumerable<>)
        || typeResult == typeof(IAsyncEnumerable<TResult>)
        || typeResult == typeof(IQueryable<TResult>)
        || typeResult == typeof(IQueryable))
        {
            throw new InvalidOperationException($"TResult of {typeResult} is not allowed, please use List<T> or T[] instead.");
        }
    }

    private static void InitCacheEntry(ICacheEntry entry, int baseExpirationSeconds)
    {
        double sec = Random.Shared.Next(baseExpirationSeconds, baseExpirationSeconds * 2);
        var expiration = TimeSpan.FromSeconds(sec);
        entry.AbsoluteExpirationRelativeToNow = expiration;
    }
}