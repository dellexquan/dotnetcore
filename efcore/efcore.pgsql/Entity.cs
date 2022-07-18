public class Book
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string AuthorName { get; set; } = null!;
    public DateTime? PubTime { get; set; }
    public double? Price { get; set; }
}

public class Person
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public string BirthPlace { get; set; } = null!;
}