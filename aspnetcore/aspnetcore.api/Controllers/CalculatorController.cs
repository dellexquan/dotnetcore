using aspnetcore.cache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace aspnetcore.api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CalculatorController : ControllerBase
{
    private readonly ICalculator calculator;
    private readonly IDistributedCache distributedCache;
    private readonly IDistributedCacheHelper distributedCacheHelper;
    private readonly IMemoryCache memoryCache;
    private readonly IMemoryCacheHelper memoryCacheHelper;
    private readonly ILogger<CalculatorController> logger;

    public CalculatorController(ICalculator calculator,
    IDistributedCache distributedCache,
    IDistributedCacheHelper distributedCacheHelper,
    IMemoryCache memoryCache,
    IMemoryCacheHelper memoryCacheHelper,
    ILogger<CalculatorController> logger)
    {
        this.calculator = calculator;
        this.distributedCache = distributedCache;
        this.distributedCacheHelper = distributedCacheHelper;
        this.memoryCache = memoryCache;
        this.memoryCacheHelper = memoryCacheHelper;
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

            //set cache expiration time
            //absolute expiration
            //e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);
            //sliding expiration
            //e.SlidingExpiration = TimeSpan.FromSeconds(10);
            //hybrid expiration
            //e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
            //e.SlidingExpiration = TimeSpan.FromSeconds(10);
            //random expiration
            e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(Random.Shared.Next(10, 15));

            return await MyDbContext.GetByIdAsync(id);
        });

        if (result == null)
        {
            return NotFound($"The book is not found.");
        }
        else
        {
            logger.LogInformation("Get from cache.");
            return result;
        }
    }
    [HttpGet()]
    public async Task<ActionResult<Book?>> TestMemoryCacheHelper(int id)
    {
        var result = await memoryCacheHelper.GetOrCreateAsync<Book?>($"MemoryBook-{id}", async (e) =>
        {
            logger.LogInformation("Get from db and cache.");
            return await MyDbContext.GetByIdAsync(id);
        }, 10);


        if (result == null)
        {
            return NotFound($"The book is not found.");
        }
        else
        {
            logger.LogInformation("Get from cache.");
            return result;
        }
    }

    [HttpGet]
    public async Task<ActionResult<Book?>> TestRedisCache(int id)
    {
        Book? result;
        var key = $"book-{id}";
        var strBook = await distributedCache.GetStringAsync(key);
        if (string.IsNullOrEmpty(strBook))
        {
            logger.LogInformation("Get from db and cache.");
            result = await MyDbContext.GetByIdAsync(id);

            logger.LogInformation("Cache to redis.");
            await distributedCache.SetStringAsync(key, JsonSerializer.Serialize(result));
        }
        else
        {
            logger.LogInformation("Get from cache.");
            result = JsonSerializer.Deserialize<Book?>(strBook);
        }
        if (result == null)
        {
            return NotFound($"The book is not found.");
        }
        else
        {
            return result;
        }
    }

    [HttpGet]
    public async Task<ActionResult<Book?>> TestDistributedCacheHelper(int id)
    {
        var result = await distributedCacheHelper.GetOrCreateAsync<Book?>($"DistributedBook-{id}", async () =>
        {
            logger.LogInformation("Get from db and cache.");
            return await MyDbContext.GetByIdAsync(id);
        }, 60);


        if (result == null)
        {
            return NotFound($"The book is not found.");
        }
        else
        {
            logger.LogInformation("Get from cache.");
            return result;
        }
    }
}