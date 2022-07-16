public interface ISample1
{
    string CurrentDateTime { get; set; }
}

public record Sample1 : ISample1
{
    public string CurrentDateTime { get; set; } = DateTime.Now.ToString();
}