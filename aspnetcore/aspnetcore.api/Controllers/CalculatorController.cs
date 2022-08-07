using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace aspnetcore.api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CalculatorController : ControllerBase
{
    private readonly ICalculator calculator;
    private readonly IMemoryCache memoryCache;
    private readonly ILogger<CalculatorController> logger;

    public CalculatorController(ICalculator calculator, IMemoryCache memoryCache, ILogger<CalculatorController> logger)
    {
        this.calculator = calculator;
        this.memoryCache = memoryCache;
        this.logger = logger;
    }

    [HttpGet]
    public int Add(int i1, int i2)
    {
        return calculator.Add(i1, i2);
    }
    [ResponseCache(Duration = 60)]
    [HttpGet]
    public DateTime Now()
    {
        return DateTime.Now;
    }

    [HttpGet()]
    public async Task<ActionResult<Book?>> GetBookById(int id)
    {
        //var result = MyDbContext.GetById(id);

        var result = await memoryCache.GetOrCreateAsync($"book-{id}", async (e) =>
        {
            logger.LogInformation("Get from db and cache.");
            return await MyDbContext.GetByIdAsync(id);
        });

        if (result == null)
        {
            return NotFound($"The book is not found.");
        }
        else
        {
            return result;
        }
    }
}