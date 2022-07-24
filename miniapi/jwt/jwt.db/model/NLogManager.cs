namespace jwt.db.model;

public class NLogManager
{
    public int Id { get; set; }
    public string Application { get; set; } = null!;
    public DateTime Logged { get; set; }
    public string Level { get; set; } = null!;
    public string Message { get; set; } = null!;
    public string? Logger { get; set; }
    public string? CallSite { get; set; }
    public string? Exception { get; set; }
}