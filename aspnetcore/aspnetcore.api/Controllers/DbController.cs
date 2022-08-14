using aspnetcore.efcore;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore.api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DbController : ControllerBase
{
    private readonly ApiDbContext apiDbContext;

    public DbController(ApiDbContext apiDbContext)
    {
        this.apiDbContext = apiDbContext;
    }

    [HttpGet]
    public int GetBooksCount()
    {
        return apiDbContext.Books.Count();
    }
}