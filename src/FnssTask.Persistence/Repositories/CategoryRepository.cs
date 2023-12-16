using Dapper;
using FnssTask.Application.Abstraction;
using FnssTask.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace FnssTask.Persistence.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly string _connectionString;

    public CategoryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("ConnectionString");
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.QueryAsync<Category>("SELECT * FROM Categories");
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Category>("SELECT * FROM Categories WHERE Id = @Id", new { Id = id });
    }

    public async Task<int> AddAsync(Category entity)
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.ExecuteAsync("INSERT INTO Categories (Name) VALUES (@Name)", entity);
    }

    public async Task<int> UpdateAsync(Category entity)
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.ExecuteAsync($"UPDATE Categories SET Name = @Name WHERE Id = @Id", entity);
    }

    public async Task<int> DeleteAsync(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.ExecuteAsync($"DELETE FROM Categories WHERE Id = @Id", new { Id = id });
    }
}

