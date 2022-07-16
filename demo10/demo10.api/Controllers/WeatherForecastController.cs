using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading.Channels;
using System.Net.Http;
using System;

namespace demo10.api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static HttpClient httpClient = new HttpClient();

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get(CancellationToken cancellationToken)
    {
        _logger.LogInformation("run api....");

        await DownloadAsync("https://www.baidu.com", 10, cancellationToken);

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    private async Task DownloadAsync(string url, int n, CancellationToken token)
    {
        using (httpClient)
        {
            for (var i = 0; i < n; i++)
            {
                var html = await httpClient.GetStringAsync(url);
                // var resp = await httpClient.GetAsync(url, token);
                await Task.Delay(1000);
                _logger.LogInformation($"Downloaded {i + 1} time.");
                if (token.IsCancellationRequested)
                {
                    _logger.LogInformation("Canceled!!!");
                    break;
                }
            }
        }

    }
}
