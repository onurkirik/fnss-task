namespace FnssTask.Domain.Entities;

public class Comment
{
    public int Id { get; set; }
    public string AuthorName { get; set; }
    public string AuthorSurname { get; set; }
    public string CommentContent { get; set; }

    //Navigation Properties
    public int ArticleId { get; set; }
    public Article Article { get; set; }
}

