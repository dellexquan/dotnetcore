using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace aspnetcore.hostedservice;

public class MyDbContextDesignTimeFactory : IDesignTimeDbContextFactory<MyDbContext>
{
    private readonly IConfigurationBuilder configurationBulder;

    public MyDbContextDesignTimeFactory()
    {
        this.configurationBulder = new ConfigurationBuilder();
    }
    public MyDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<MyDbContext>();
        configurationBulder.AddJsonFile("appsettings.json");
        var configuration = configurationBulder.Build();
        var connStr = configuration.GetSection("ConnectionStrings").GetSection("Default").Value;
        builder.UseSqlite(connStr);
        var ctx = new MyDbContext(builder.Options);
        return ctx;
    }
}