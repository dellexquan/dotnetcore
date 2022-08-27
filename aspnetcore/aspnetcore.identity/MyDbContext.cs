using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace aspnetcore.identity;

public class MyDbContext : IdentityDbContext<MyUser, MyRole, long>
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {

    }
}