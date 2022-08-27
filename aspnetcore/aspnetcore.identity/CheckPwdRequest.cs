namespace aspnetcore.identity;

public record CheckPwdRequest(string UserName, string Password);