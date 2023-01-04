using Microsoft.EntityFrameworkCore;
using ProductAPI.Application.Common.Interfaces;
using ProductAPI.Infrastructure.Context;

namespace ProductAPI.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected DbSet<T> _entities;

    public GenericRepository(ProductPurchaseContext context)
    {
        _entities = context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await _entities.AddAsync(entity);
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _entities.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _entities.FindAsync(id);
    }

    public void Update(T entity)
    {
        throw new NotImplementedException();
    }
}