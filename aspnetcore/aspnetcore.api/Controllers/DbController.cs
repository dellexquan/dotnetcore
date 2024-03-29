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
    [HttpGet]
    public async Task<Book?> GetBookById(long Id)
    {
        return await apiDbContext.Books.FindAsync(Id);
    }
    [HttpPost]
    public async Task<Book> NewBookAsync(Book book)
    {
        apiDbContext.Books.Add(book);
        await apiDbContext.SaveChangesAsync();

        return book;
    }
}