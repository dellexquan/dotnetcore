using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddTransient<ISample1, Sample1>();

using (var sp = services.BuildServiceProvider())
{
    var sample1 = sp.GetRequiredService<ISample1>();
    System.Console.WriteLine($"{sample1.CurrentDateTime}");
}