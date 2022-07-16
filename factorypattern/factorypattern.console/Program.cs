using Microsoft.Extensions.DependencyInjection;
using Samples;
using Factories;

var services = new ServiceCollection();
//services.AddTransient<ISample1, Sample1>();
//services.AddSingleton<Func<ISample1>>(x => () => { return x.GetRequiredService<ISample1>(); });
services.AddAbstractFactory<ISample1, Sample1>();
services.AddAbstractFactory<ISample2, Sample2>();
services.AddGenericClassWithDataFactory();
services.AddVehicleFactory();

using (var sp = services.BuildServiceProvider())
{
    var vehicles = new string[] { "Car", "Truck", "Van", "Truck", "Car" };
    for (var i = 0; i < 5; i++)
    {
        var factory1 = sp.GetRequiredService<IAbstractFactory<ISample1>>();
        var sample1 = factory1.Create();
        var factory2 = sp.GetRequiredService<IAbstractFactory<ISample2>>();
        var sample2 = factory2.Create();
        System.Console.WriteLine("Sample1: " + sample1.CurrentDateTime);
        System.Console.WriteLine("Sample2: " + sample2.RandomValue);
        var factory3 = sp.GetRequiredService<IUserDataFactory>();
        var userData = factory3.Create("Dellex" + i);
        System.Console.WriteLine("User Data: " + userData.Name);
        var factory4 = sp.GetRequiredService<IVehicleFactory>();
        var vehicle = factory4.Create(vehicles[i]);
        System.Console.WriteLine(vehicle.Start());
        await Task.Delay(1000);
    }
}