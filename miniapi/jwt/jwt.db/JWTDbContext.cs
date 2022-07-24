using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using jwt.db.model;
namespace jwt.db;
public class JWTDbContext : DbContext
{
    public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
    {
        builder.AddConsole();
    });

    public DbSet<Commodity>? Commodity { get; set; }
    public DbSet<Company>? Company { get; set; }
    public DbSet<SysUser>? SysUser { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=jwt.db");
        }
        optionsBuilder.UseLoggerFactory(MyLoggerFactory);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}
