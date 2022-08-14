using Microsoft.EntityFrameworkCore;

namespace aspnetcore.efcore;

public class ApiDbContext : DbContext
{
    public DbSet<Book> Books { get; set; } = null!;

    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //base.OnConfiguring(optionsBuilder);
        //optionsBuilder.UseSqlite();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}