using FnssTask.Domain.Entities;

namespace FnssTask.Presentation.Models.Article;

public class ArticleCreateDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int CategoryId { get; set; }
    public IEnumerable<Category> Categories { get; set; }
}

