using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ddd.mediatr.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IMediator mediator;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator)
    {
        _logger = logger;
        this.mediator = mediator;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>>   Get()
    {
        await mediator.Publish(new PostNotification("Hello! " + DateTime.Now));

        using var ctx = new MyDbContext(mediator);
        var user = new User("Dellex", Gender.Male);
        user.ChangePassword("123456");
        user.ChangeUserName("abc");
        ctx.Users.Add(user);
        await ctx.SaveChangesAsync();

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
