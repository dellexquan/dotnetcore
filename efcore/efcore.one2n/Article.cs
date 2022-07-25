public class Article
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public List<Comment> Comments { get; set; } = new List<Comment>();
}