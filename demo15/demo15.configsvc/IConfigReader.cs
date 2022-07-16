namespace demo15.configsvc;

public interface IConfigReader
{
    public string? GetValue(string name);
}