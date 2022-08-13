using aspnetcore.common;
namespace aspnetcore.cache;

public interface IExpirationRandom
{
    TimeSpan Next(int baseExpirationSeconds);
}

public class ExpirationRandom : IExpirationRandom
{
    public TimeSpan Next(int baseExpirationSeconds)
    {
        double sec = Random.Shared.NextDouble(baseExpirationSeconds, baseExpirationSeconds * 2);
        return TimeSpan.FromSeconds(sec);
    }
}