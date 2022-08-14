namespace aspnetcore.api;

public record SmtpSettings
{
    public string Server { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
}