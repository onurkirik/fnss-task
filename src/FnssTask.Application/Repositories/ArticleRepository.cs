using FnssTask.Application.Concrete;
using FnssTask.Domain.Entities;

namespace FnssTask.Application.Repositories;

public class ArticleRepository : Repository<Article>
{
    public ArticleRepository(string connectionString) : base(connectionString) { }
}