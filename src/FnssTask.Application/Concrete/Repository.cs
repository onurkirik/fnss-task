using Dapper;
using FnssTask.Application.Abstraction;
using MySqlConnector;

namespace FnssTask.Application.Concrete;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly string _connectionString;

    public Repository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.QueryAsync<T>("SELECT * FROM " + typeof(T).Name);
    }

    public async Task<T> GetByIdAsync(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<T>("SELECT * FROM " + typeof(T).Name + " WHERE Id = @Id", new { Id = id });
    }

    public async Task<int> InsertAsync(T entity)
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.ExecuteAsync($"INSERT INTO {typeof(T).Name} VALUES (@Id, @Name, @OtherProperties)", entity);
    }

    public async Task<int> UpdateAsync(T entity)
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.ExecuteAsync($"UPDATE {typeof(T).Name} SET Name = @Name, OtherProperties = @OtherProperties WHERE Id = @Id", entity);
    }

    public async Task<int> DeleteAsync(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.ExecuteAsync($"DELETE FROM {typeof(T).Name} WHERE Id = @Id", new { Id = id });
    }
}
