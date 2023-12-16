using Dapper;
using FnssTask.Application.Abstraction;
using FnssTask.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace FnssTask.Persistence.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly string _connectionString;

    public ArticleRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("ConnectionString");
    }

    public async Task<IEnumerable<Article>> GetAllAsync()
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.QueryAsync<Article>("SELECT * FROM Articles");
    }

    public async Task<Article> GetByIdAsync(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Article>("SELECT * FROM Articles WHERE Id = @Id", new { Id = id });
    }

    public async Task<int> AddAsync(Article entity)
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.ExecuteAsync($"INSERT INTO Articles VALUES (@Title, @Content, @CategoryId)", entity);
    }

    public async Task<int> UpdateAsync(Article entity)
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.ExecuteAsync($"UPDATE Articles SET Title = @Title, Content = @Content, CategoryId = @CategoryId WHERE Id = @Id", entity);
    }

    public async Task<int> DeleteAsync(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.ExecuteAsync($"DELETE FROM Articles WHERE Id = @Id", new { Id = id });
    }
}