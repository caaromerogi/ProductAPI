using System.Linq.Expressions;

namespace ProductAPI.Application.Common.Interfaces;

public interface IGenericRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T entity);
    void Update(T entity);
    Task Delete(int id);
}