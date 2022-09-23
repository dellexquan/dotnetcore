public class Blog
{
    public int Id { get; set; }
    public MultiLangString Title { get; set; } = null!;
    public MultiLangString Body { get; set; } = null!;

    public override string ToString()
    {
        return $"Id: {this.Id}, Title: {this.Title}, Body: {this.Body}";
    }
}