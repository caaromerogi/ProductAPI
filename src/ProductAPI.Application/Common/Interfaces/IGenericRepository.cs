using System.Linq.Expressions;

namespace ProductAPI.Application.Common.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int first = 0, int offset = 0);
    Task<T> GetByIdAsync(params object[] keys);
    Task AddAsync(T entity);
    void Update(T entity);
    Task Delete(int id);
    
}