namespace Parking.Repositories;
public interface IRepository<T>
{
    IQueryable<T> Get();
    // Task<T>GetById(int id);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task AddAsync(T entity);
}