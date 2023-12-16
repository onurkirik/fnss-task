using FnssTask.Application.Concrete;
using FnssTask.Domain.Entities;

namespace FnssTask.Application.Repositories;

public class CategoryRepository : Repository<Article>
{
    public CategoryRepository(string connectionString) : base(connectionString) { }
}

