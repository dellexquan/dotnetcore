using ddd.usermanager.domain;
using Microsoft.EntityFrameworkCore;

namespace ddd.usermanager.efcore;

public class UserDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserLoginHistory> UserLoginHistories { get; set; } = null!;

    public UserDbContext(DbContextOptions<UserDbContext> opt) : base(opt)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }

}