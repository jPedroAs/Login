using Microsoft.EntityFrameworkCore;
using Login.Infra.Context;

namespace Login.Repositories;

public class GenericRepository<T> : IRepository<T> where T : class
{
    private readonly ParkingContext _context;

    public GenericRepository(ParkingContext context){
        _context = context;
    }

    public async Task AddAsync(T entity)
    {
       await _context.Set<T>().AddAsync(entity);
    }
    public Task UpdateAsync(T entity)
    {
        return Task.FromResult(_context.Update(entity));
    }
     public  Task DeleteAsync(T entity)
     {
        return Task.FromResult(_context.Remove(entity));
     }
     public  IQueryable<T> Get()
    {
       return _context.Set<T>().AsQueryable();
    }
    //  public async Task<T> GetById(int id)
    // {
    //    return await  _context.Set<T>().FindAsync(id);
    // }
}