using Dapper;

namespace ProductAPI.Application.Common.Interfaces;

public interface IDapperService<T>
{
    public Task<IEnumerable<T>> Get(string query, DynamicParameters parameters);    
}