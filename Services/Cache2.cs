using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;


public class CacheService222(IDistributedCache distributedCache) : ICacheService
{
    private readonly IDistributedCache _distributedCache = distributedCache;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    public async Task<T> GetAsync<T>(string key, CancellationToken token = default) where T : class
    {
        var cache = await _distributedCache.GetStringAsync(key, token);
        if (cache is null)
        {
            return null;
        }

        try
        {
            return JsonSerializer.Deserialize<T>(cache, JsonOptions);
        }
        catch (JsonException)
        {
            await RemoveAsync(key, token);
            return null;
        }
    }

    public async Task<T> GetOrSetAsync<T>(string key, Func<CancellationToken, Task<T>> factory, TimeSpan? expiration = null, CancellationToken token = default) where T : class
    {
        T? cache = await GetAsync<T>(key, token);
        if (cache is not null)
        {
            return cache;
        }

        T value = await factory(token);
        await SetAsync(key, value, expiration, token);
        return value;
    }

    public async Task RemoveAsync(string key, CancellationToken token = default)
    {
        await _distributedCache.RemoveAsync(key, token);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken token = default) where T : class
    {
        var cache = JsonSerializer.Serialize(value, JsonOptions);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromSeconds(15)
        };
        await _distributedCache.SetStringAsync(key, cache, options, token);
    }
}