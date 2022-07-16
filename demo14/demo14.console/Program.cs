using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
// services.AddTransient<TestServiceImpl>();
// services.AddSingleton<TestServiceImpl>();
// services.AddScoped<TestServiceImpl>();
services.AddSingleton<ITestService, TestServiceImpl>();
using (var sp = services.BuildServiceProvider())
{
    // var t = sp.GetService<TestServiceImpl>();
    // t!.Name = "Dellex";
    // t.SayHi();

    // var t1 = sp.GetService<TestServiceImpl>();
    // Console.WriteLine(object.ReferenceEquals(t, t1));
    // t1!.Name = "Tom";
    // t1.SayHi();

    // t.SayHi();
    // using (var scope = sp.CreateScope())
    // {
    //     var t2 = scope.ServiceProvider.GetService<TestServiceImpl>();
    //     t2!.Name = "Dellex";
    //     t2.SayHi();
    // }

    // using (var scope2 = sp.CreateScope())
    // {
    //     var t3 = scope2.ServiceProvider.GetService<TestServiceImpl>();
    //     t3!.Name = "Mary";
    //     t3.SayHi();/
    //}
    var ts1 = sp.GetService<ITestService>();
    ts1!.Name = "Tom";
    ts1.SayHi();
}

// ITestService testService = new TestServiceImpl();
// testService.Name = "Dellex";
// testService.SayHi();

interface ITestService
{
    public string? Name { get; set; }
    public void SayHi();
}

class TestServiceImpl : ITestService
{
    public string? Name { get; set; }
    public void SayHi()
    {
        Console.WriteLine($"Hello, {this.Name}!");
    }
}
