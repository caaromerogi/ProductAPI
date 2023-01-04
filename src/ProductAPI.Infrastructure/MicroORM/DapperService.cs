using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProductAPI.Application.Common.Interfaces;

namespace ProductAPI.Infrastructure.MicroORM;

public class DapperService<T> : IDapperService<T>
{
    private readonly IConfiguration _configuration;

    public DapperService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IEnumerable<T>> Get(string query, DynamicParameters parameters)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("ProductConnection"));
        await connection.OpenAsync();
        return await connection.QueryAsync<T>(query, parameters);
    }

    public async Task<IEnumerable<T>> Get(string query)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("ProductConnection"));
        await connection.OpenAsync();
        return await connection.QueryAsync<T>(query);
    }
}
