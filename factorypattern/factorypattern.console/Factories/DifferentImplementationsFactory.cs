using Microsoft.Extensions.DependencyInjection;
using Samples;

namespace Factories;

public static class DifferentImplementationFactoryExtension
{
    public static void AddVehicleFactory(this ServiceCollection services)
    {
        services.AddTransient<IVehicle, Car>();
        services.AddTransient<IVehicle, Truck>();
        services.AddTransient<IVehicle, Van>();
        services.AddSingleton<Func<IEnumerable<IVehicle>>>(x => () => x.GetRequiredService<IEnumerable<IVehicle>>());
        services.AddSingleton<IVehicleFactory, VehicleFactory>();
    }
}

public class VehicleFactory : IVehicleFactory
{
    private readonly Func<IEnumerable<IVehicle>> factory;
    public VehicleFactory(Func<IEnumerable<IVehicle>> factory)
    {
        this.factory = factory;
    }

    public IVehicle Create(string name)
    {
        var set = factory();
        var output = set.Where(x => x.VehicleType == name).First();
        return output;
    }

}

public interface IVehicleFactory
{
    IVehicle Create(string name);
}