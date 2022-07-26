public class Comment
{
    public long Id { get; set; }
    public string Message { get; set; } = null!;
    public long ArticleId { get; set; }
    public Article Article { get; set; } = null!;
}