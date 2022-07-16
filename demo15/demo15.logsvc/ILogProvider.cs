namespace demo15.logsvc;

public interface ILogProvider
{
    public void LogError(string msg);
    public void LogInfo(string msg);
}