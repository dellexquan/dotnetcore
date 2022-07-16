using Microsoft.Extensions.DependencyInjection;
namespace Factories;
public static class AbstractFactoryExtension
{
    public static void AddAbstractFactory<TInterface, TImplementation>(this IServiceCollection services)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        services.AddTransient<TInterface, TImplementation>();
        services.AddSingleton<Func<TInterface>>(x => () => x.GetRequiredService<TInterface>());
        services.AddSingleton<IAbstractFactory<TInterface>, AbstractFactory<TInterface>>();
    }
}


public interface IAbstractFactory<T>
{
    T Create();
}

public class AbstractFactory<T> : IAbstractFactory<T>
{
    private readonly Func<T> factory;
    public AbstractFactory(Func<T> factory)
    {
        this.factory = factory;
    }

    public T Create()
    {
        return factory();
    }
}