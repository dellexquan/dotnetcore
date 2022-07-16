namespace demo15.configsvc;

public class EnvVarConfigService : IConfigService
{
    public string? GetValue(string name)
    {
        return System.Environment.GetEnvironmentVariable(name);
    }
}