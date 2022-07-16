using Microsoft.Extensions.DependencyInjection;
using Factories;

var services = new ServiceCollection();
//services.AddTransient<ISample1, Sample1>();
//services.AddSingleton<Func<ISample1>>(x => () => { return x.GetRequiredService<ISample1>(); });
services.AddAbstractFactory<ISample1, Sample1>();
services.AddAbstractFactory<ISample2, Sample2>();

using (var sp = services.BuildServiceProvider())
{
    for (var i = 0; i < 5; i++)
    {
        var factory1 = sp.GetRequiredService<IAbstractFactory<ISample1>>();
        var sample1 = factory1.Create();
        var factory2 = sp.GetRequiredService<IAbstractFactory<ISample2>>();
        var sample2 = factory2.Create();
        System.Console.WriteLine("Sample1: " + sample1.CurrentDateTime);
        System.Console.WriteLine("Sample2: " + sample2.RandomValue);
        await Task.Delay(1000);
    }
}