public class Blog
{
    public int Id { get; set; }
    public MultiLangString Title { get; set; } = null!;
    public MultiLangString Body { get; set; } = null!;
}