using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

public interface IHasId
{
    Guid Id { get; }
}

public class Product : IHasId
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// --- Bug 1 ---
public class Repository<T> where T : IHasId, new()
{
    private readonly List<T> _items = [];

    public void Add(T item) => _items.Add(item);

    public T CreateEmpty()
    {
        return new T();
    }
}
// тут была ошибка потмоу что мы в ретерне указали то что вовзаращем в обэкт а 
//сверзу было укзаано только то что мы имеем Т и то что он долежн иметь айди

// --- Bug 2 ---
public class Cache<T> where T : class
{
    private readonly Dictionary<string, T> _storage = [];

    public void Set(string key, T value) => _storage[key] = value;

    public T Get(string key)
    {
        return _storage.TryGetValue(key, out var value) ? value : default;
    }
}

// --- Bug 3 ---
public interface IProducer<out T>
{
    T Produce();
}

public class ProductProducer : IProducer<Product>
{
    public Product Produce() => new() { Id = Guid.NewGuid() };
}

public class Demo
{
    public void TestCovariance()
    {
        IProducer<Product> productProducer = new ProductProducer();
        IProducer<object> objectProducer = productProducer;
    }
}

// --- Bug 4 ---

public class Matcher
{
    public List<T> FindMatching<T>(List<T> items, Guid targetId) where T : IHasId
    {

        return items.Where(x => x.Id == targetId).ToList();

    }
}

// --- Bug 5 ---
public class SafeGetter<T> where T : struct
{
    public T? GetOrDefault(List<T> items, int index)
    {
        if (index < 0 || index >= items.Count)
            return null;

        return items[index];
    }

    public T GetValueOrThrow(T? nullable)
    {
        return (T)nullable;
    }
}