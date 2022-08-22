using Microsoft.EntityFrameworkCore;

namespace aspnetcore.filter;

public class MyDbContext : DbContext
{
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Person> Persons { get; set; } = null!;

    public MyDbContext(DbContextOptions<MyDbContext> opt) : base(opt)
    {

    }
}