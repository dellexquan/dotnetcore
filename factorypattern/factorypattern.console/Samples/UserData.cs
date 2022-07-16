namespace Samples;
public class UserData : IUserData
{
    public string? Name { get; set; }
}

public interface IUserData
{
    string? Name { get; set; }
}