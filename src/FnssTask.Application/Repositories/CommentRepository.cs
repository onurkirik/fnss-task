using FnssTask.Application.Concrete;
using FnssTask.Domain.Entities;

namespace FnssTask.Application.Repositories;

public class CommentRepository : Repository<Comment>
{
    public CommentRepository(string connectionString) : base(connectionString) { }
}

