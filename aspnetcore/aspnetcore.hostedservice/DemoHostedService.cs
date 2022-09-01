namespace aspnetcore.hostedservice;

public class DemoHostedService : BackgroundService
{
    private const string FILE_NAME = "1.txt";
    private readonly IServiceScope serviceScope;

    public DemoHostedService(IServiceScopeFactory serviceScopeFactory)
    {
        this.serviceScope = serviceScopeFactory.CreateScope();
    }

    public override void Dispose()
    {
        this.serviceScope.Dispose();
        base.Dispose();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var testService = serviceScope.ServiceProvider.GetRequiredService<TestService>();

        System.Console.WriteLine("Hosted service start.");
        await Task.Delay(3000); //no sleep
        await File.WriteAllTextAsync(FILE_NAME, $"Hosted service start! {DateTime.Now}");
        System.Console.WriteLine("Write file completed.");
        await Task.Delay(3000);
        var content = await File.ReadAllTextAsync(FILE_NAME);
        await Task.Delay(3000);
        System.Console.WriteLine(content);
        var result = testService.Add(1, 1);
        await Task.Delay(3000);
        System.Console.WriteLine($"Test service result: {result}");
    }
}