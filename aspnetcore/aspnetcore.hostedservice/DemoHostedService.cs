namespace aspnetcore.hostedservice;

public class DemoHostedService : BackgroundService
{
    private const string FILE_NAME = "1.txt";
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        System.Console.WriteLine("Hosted service start.");
        await Task.Delay(3000); //no sleep
        await File.WriteAllTextAsync(FILE_NAME, $"Hosted service start! {DateTime.Now}");
        System.Console.WriteLine("Write file completed.");
        await Task.Delay(3000);
        var content = await File.ReadAllTextAsync(FILE_NAME);
        await Task.Delay(3000);
        System.Console.WriteLine(content);
    }
}