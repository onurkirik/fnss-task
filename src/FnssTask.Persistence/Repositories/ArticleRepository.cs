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
        var query = @"
                        SELECT 
                        A.Id, 
                        A.Title, 
                        A.Content, 
                        A.CategoryId, 
                        C.Id AS CategoryId, 
                        C.Name AS CategoryName, 
                        COALESCE(JSON_ARRAYAGG(JSON_OBJECT('id', CO.Id, 'authorName', CO.AuthorName, 'authorSurname', CO.AuthorSurname, 'content', CO.CommentContent)), '[]') AS Comments
                        FROM 
                            Articles A
                        LEFT JOIN 
                            Categories C ON A.CategoryId = C.Id
                        LEFT JOIN 
                            Comments CO ON A.Id = CO.ArticleId
                        GROUP BY 
                            A.Id";

        var articles = await connection.QueryAsync<Article>(query);

        return articles;
    }

    public async Task<Article> GetByIdAsync(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        var query = @"
                    SELECT 
                        A.Id, 
                        A.Title, 
                        A.Content, 
                        A.CategoryId, 
                        C.Id AS CategoryId, 
                        C.Name AS CategoryName, 
                        COALESCE(JSON_ARRAYAGG(JSON_OBJECT('id', CO.Id, 'authorName', CO.AuthorName, 'authorSurname', CO.AuthorSurname, 'content', CO.CommentContent)), '[]') AS Comments
                    FROM 
                        Articles A
                    LEFT JOIN 
                        Categories C ON A.CategoryId = C.Id
                    LEFT JOIN 
                        Comments CO ON A.Id = CO.ArticleId
                    WHERE 
                        A.Id = @Id
                    GROUP BY 
                        A.Id";

        var article = await connection.QueryFirstOrDefaultAsync<Article>(query, new { Id = id });

        return article;
    }

    public async Task<int> AddAsync(Article entity)
    {
        using var connection = new MySqlConnection(_connectionString);
        return await connection.ExecuteAsync(
            "INSERT INTO Articles (Title, Content, CategoryId) VALUES (@Title, @Content, @CategoryId)",
            new { entity.Title, entity.Content, entity.CategoryId }
        );
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