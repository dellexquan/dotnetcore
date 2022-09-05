namespace aspnetcore.identity;
public record LoginRequest(string Email, string Password, string ConfirmPassword);