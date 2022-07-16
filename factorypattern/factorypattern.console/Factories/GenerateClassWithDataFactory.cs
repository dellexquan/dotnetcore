using Microsoft.Extensions.DependencyInjection;
using Samples;

namespace Factories;

public static class GenerateClassWithDataFactoryExtension
{
    public static void AddGenericClassWithDataFactory(this ServiceCollection service)
    {
        service.AddTransient<IUserData, UserData>();
        service.AddSingleton<Func<IUserData>>(x => () => x.GetRequiredService<IUserData>());
        service.AddSingleton<IUserDataFactory, UserDataFactory>();
    }
}

public interface IUserDataFactory
{
    IUserData Create(string name);
}

public class UserDataFactory : IUserDataFactory
{
    private readonly Func<IUserData> factory;
    public UserDataFactory(Func<IUserData> factory)
    {
        this.factory = factory;
    }

    public IUserData Create(string name)
    {
        var output = factory();
        output.Name = name;
        return output;
    }
}