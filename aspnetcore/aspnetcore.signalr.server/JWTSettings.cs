namespace aspnetcore.jwt;

public class JWTSettings
{
    public string SecKey { get; set; } = null!;
    public int ExpireSeconds { get; set; }
}