public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(Guid Id);
    Task Add(T obj);
    Task Update(T entity);
    Task Remove(T entity);

}