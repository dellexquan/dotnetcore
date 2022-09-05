namespace aspnetcore.hostedservice;

public class ScheduledHostService : BackgroundService
{
    private readonly IServiceScope serviceScope;
    public ScheduledHostService(IServiceScopeFactory serviceScopeFactory)
    {
        this.serviceScope = serviceScopeFactory.CreateScope();
    }
    public override void Dispose()
    {
        serviceScope.Dispose();
        base.Dispose();
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<MyDbContext>();
            while (!stoppingToken.IsCancellationRequested)
            {
                long c = dbContext.Users.LongCount();
                await File.WriteAllTextAsync("userc.txt", c.ToString());
                await Task.Delay(5000);
                System.Console.WriteLine($"Export success: {DateTime.Now}");
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
        }
    }
}