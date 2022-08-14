namespace aspnetcore.efcore;
public class Book
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string AuthorName { get; set; } = null!;
    public double Price { get; set; }
    public DateTime PubDate { get; set; }
}
