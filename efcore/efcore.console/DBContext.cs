using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class EntityDbContext : DbContext
{
    public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Person> Persons { get; set; } = null!;
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //var connStr = @"Data Source=entity.db";
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("appSettings.json");
        var config = builder.Build();
        var connStr = config["connectionString"];
        optionsBuilder.UseSqlite(connStr);
        optionsBuilder.UseLoggerFactory(loggerFactory);
        //optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}