using System.Collections;

namespace aspnetcore.cache;

public interface ICacheValueTypeValidator
{
    void ValidateValueType<TResult>();
}


public class CacheValueTypeValidator : ICacheValueTypeValidator
{
    public void ValidateValueType<TResult>()
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
}
