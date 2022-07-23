namespace jwt.api.models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreateTime { get; set; }
}