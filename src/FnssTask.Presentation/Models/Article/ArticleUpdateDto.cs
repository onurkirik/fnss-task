using FnssTask.Domain.Entities;

namespace FnssTask.Presentation.Models.Article;

public class ArticleUpdateDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public IEnumerable<Category> Categories { get; set; }
}

