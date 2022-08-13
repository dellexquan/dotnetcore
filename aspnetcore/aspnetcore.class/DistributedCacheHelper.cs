using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace aspnetcore.cache;

public interface IDistributedCacheHelper
{
    TResult? GetOrCreate<TResult>(string cacheKey, Func<TResult?> valueFactory, int expireSeconds);
    Task<TResult?> GetOrCreateAsync<TResult>(string cacheKey, Func<Task<TResult?>> valueFactory, int expireSeconds);
    void Remove(string cacheKey);
    Task RemoveAsync(string cacheKey);
}

public class DistributedCacheHelper : IDistributedCacheHelper
{
    private readonly IDistributedCache distributedCache;
    private readonly IExpirationRandom expirationRandom;
    private readonly ICacheValueTypeValidator cacheValueTypeValidator;

    public DistributedCacheHelper(IDistributedCache distributedCache,
    IExpirationRandom expirationRandom,
    ICacheValueTypeValidator cacheValueTypeValidator)
    {
        this.distributedCache = distributedCache;
        this.expirationRandom = expirationRandom;
        this.cacheValueTypeValidator = cacheValueTypeValidator;
    }

    public TResult? GetOrCreate<TResult>(string cacheKey, Func<TResult?> valueFactory, int expireSeconds = 60)
    {
        cacheValueTypeValidator.ValidateValueType<TResult>();
        TResult? result;
        var strObj = distributedCache.GetString(cacheKey);
        if (string.IsNullOrEmpty(strObj))
        {
            result = valueFactory.Invoke();
            var options = InitCacheEntryOptions(expireSeconds);
            distributedCache.SetString(cacheKey, JsonSerializer.Serialize(result), options);
        }
        else
        {
            result = JsonSerializer.Deserialize<TResult?>(strObj);
        }

        return result;
    }

    public async Task<TResult?> GetOrCreateAsync<TResult>(string cacheKey, Func<Task<TResult?>> valueFactory, int expireSeconds = 60)
    {
        cacheValueTypeValidator.ValidateValueType<TResult>();
        TResult? result;
        var strObj = await distributedCache.GetStringAsync(cacheKey);
        if (string.IsNullOrEmpty(strObj))
        {
            result = await valueFactory.Invoke();
            var options = InitCacheEntryOptions(expireSeconds);
            await distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(result), options);
        }
        else
        {
            result = JsonSerializer.Deserialize<TResult?>(strObj);
        }

        return result;
    }

    public void Remove(string cacheKey)
    {
        distributedCache.Remove(cacheKey);
    }

    public async Task RemoveAsync(string cacheKey)
    {
        await distributedCache.RemoveAsync(cacheKey);
    }

    private DistributedCacheEntryOptions InitCacheEntryOptions(int baseExpirationSeconds)
    {
        var expiration = expirationRandom.Next(baseExpirationSeconds);
        return new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = expiration
        };
    }
}