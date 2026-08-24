using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

public interface ICacheService
{
    Task<T> GetAsync<T>(string key, CancellationToken token = default) where T : class;
    Task<T> GetOrSetAsync<T>(string key, Func<CancellationToken, Task<T>> factory, TimeSpan? expiration = null, CancellationToken token = default) where T : class;
    Task RemoveAsync(string key, CancellationToken token = default);
    Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken token = default) where T : class;
}