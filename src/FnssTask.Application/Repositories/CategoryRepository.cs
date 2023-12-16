using FnssTask.Application.Concrete;
using FnssTask.Domain.Entities;

namespace FnssTask.Application.Repositories;

public class CategoryRepository : Repository<Category>
{
    public CategoryRepository(string connectionString) : base(connectionString) { }
}

