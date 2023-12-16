using Dapper;
using FnssTask.Application.Abstraction;
using FnssTask.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace FnssTask.Persistence.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly string _connectionString;

    public CommentRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("ConnectionString");
    }

    public async Task<IEnumerable<Comment>> GetAllAsync()
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.QueryAsync<Comment>("SELECT * FROM Comments");
    }

    public async Task<Comment> GetByIdAsync(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Comment>("SELECT * FROM Comments WHERE Id = @Id", new { Id = id });
    }

    public async Task<int> AddAsync(Comment entity)
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.ExecuteAsync(
            $"INSERT INTO Comments VALUES (@AuthorName, @AuthorSurname, @CommentContent, @ArticleId)",
            entity);
    }

    public async Task<int> UpdateAsync(Comment entity)
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.ExecuteAsync(
            $"UPDATE Comments SET AuthorName = @AuthorName, AuthorSurname = @AuthorSurname, CommentContent = @CommentContent, ArticleId = @ArticleId WHERE Id = @Id",
            entity);
    }

    public async Task<int> DeleteAsync(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.ExecuteAsync($"DELETE FROM Comments WHERE Id = @Id", new { Id = id });
    }
}

