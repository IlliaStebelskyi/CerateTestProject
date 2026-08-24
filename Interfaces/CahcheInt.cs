public interface CacheService2
{
    public Task<T> GetOrSetAsync<T>(string key, Func<CancellationToken, Task<T>> factory, TimeSpan? exp = null, CancellationToken token = default) where T : class;
    public Task RemoveCache(string key, CancellationToken token = default);
    public Task<T> SetAsync<T>(string key, TimeSpan? time = null, CancellationToken token = default) where T : class;
    public Task<T> GetAsync<T>(string key, CancellationToken token = default)where T : class;
    
}