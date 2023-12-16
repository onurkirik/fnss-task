namespace FnssTask.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }

    //Navigation Properties
    public List<Article> Articles { get; set; }
}

