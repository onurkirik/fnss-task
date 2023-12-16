namespace FnssTask.Domain.Entities;

public class Article
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    //Navigation Properties
    public int CagetoryId { get; set; }
    public Category Category { get; set; }

    public List<Comment> Comments { get; set; }
}

