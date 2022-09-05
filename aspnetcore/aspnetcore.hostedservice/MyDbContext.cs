using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace aspnetcore.hostedservice;

public class MyDbContext : IdentityDbContext<MyUser, MyRole, long>
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {

    }
}