using FnssTask.Domain.Entities;

namespace FnssTask.Application.Abstraction;

public interface ICommentRepository : IRepository<Comment>
{
    Task<IEnumerable<Comment>> GetAllWithArticleAsync(int id);
}
