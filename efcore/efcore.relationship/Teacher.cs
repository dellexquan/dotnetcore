public class Teacher
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Student> Students { get; set; } = new List<Student>();
}