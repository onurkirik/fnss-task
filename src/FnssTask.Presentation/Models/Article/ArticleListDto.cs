using FnssTask.Domain.Entities;

namespace FnssTask.Presentation.Models.Article;

public class ArticleListDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Category Category { get; set; }
}

