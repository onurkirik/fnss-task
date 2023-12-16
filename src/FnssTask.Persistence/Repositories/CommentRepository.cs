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
        return await connection.QueryAsync<Comment>("SELECT * FROM Articles");
    }

    public async Task<Comment> GetByIdAsync(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Comment>("SELECT * FROM Articles WHERE Id = @Id", new { Id = id });
    }

    public async Task<int> AddAsync(Comment entity)
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.ExecuteAsync($"INSERT INTO Articles VALUES (@Id, @Name, @OtherProperties)", entity);
    }

    public async Task<int> UpdateAsync(Comment entity)
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.ExecuteAsync($"UPDATE Articles SET Name = @Name, OtherProperties = @OtherProperties WHERE Id = @Id", entity);
    }

    public async Task<int> DeleteAsync(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.ExecuteAsync($"DELETE FROM Articles WHERE Id = @Id", new { Id = id });
    }
}

