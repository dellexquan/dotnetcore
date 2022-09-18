using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite("Data Source=ddd.db");
    }
}