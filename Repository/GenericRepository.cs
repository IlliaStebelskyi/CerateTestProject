using Microsoft.EntityFrameworkCore;

public class GenericRepository<T>(ApplicationDb applicationDb) : IGenericRepository<T> where T : class
{
    private readonly ApplicationDb _applicationDb = applicationDb;
    private readonly DbSet<T> _entities = applicationDb.Set<T>();
    public async Task Add(T obj)
    {
        _entities.Add(obj);
        await _applicationDb.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _entities.ToListAsync();
    }

    public async Task<T?> GetById(Guid Id)
    {
        return await _entities.FindAsync(Id);
    }

    public async Task Remove(T entity)
    {
        _entities.Remove(entity);
        await _applicationDb.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        _entities.Update(entity);
        await _applicationDb.SaveChangesAsync();
    }
}