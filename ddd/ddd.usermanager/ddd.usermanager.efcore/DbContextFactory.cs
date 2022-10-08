using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ddd.usermanager.efcore;

public class DbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
{
    public UserDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<UserDbContext>();
        builder.UseSqlite("Data Source=../ddd.usermanager.api/ddd.db");
        return new UserDbContext(builder.Options);
    }
}