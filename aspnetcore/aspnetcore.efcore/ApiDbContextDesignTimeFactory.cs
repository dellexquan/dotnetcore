using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace aspnetcore.efcore;

internal class ApiDbContextDesignTimeFactory : IDesignTimeDbContextFactory<ApiDbContext>
{
    private readonly IConfigurationBuilder configurationBulder;

    public ApiDbContextDesignTimeFactory()
    {
        this.configurationBulder = new ConfigurationBuilder();
    }
    public ApiDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ApiDbContext>();
        configurationBulder.AddJsonFile("appsettings.json");
        var configuration = configurationBulder.Build();
        var connStr = configuration.GetSection("ConnectionStrings").GetSection("Default").Value;
        builder.UseSqlite(connStr);
        var ctx = new ApiDbContext(builder.Options);
        return ctx;
    }
}