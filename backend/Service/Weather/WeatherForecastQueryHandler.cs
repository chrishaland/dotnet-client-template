using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Host.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/weatherforecast")]
    public class WeatherForecastQueryHandler : QueryHandlerBase<WeatherForecastRequest, WeatherForecastResponse>
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastQueryHandler> _logger;

        public WeatherForecastQueryHandler(ILogger<WeatherForecastQueryHandler> logger)
        {
            _logger = logger;
        }

        public override async Task<ActionResult<WeatherForecastResponse>> Execute([FromBody] WeatherForecastRequest request, CancellationToken ct)
        {
            var rng = new Random();
            var weatherForecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray();

            await Task.CompletedTask;
            return Ok(new WeatherForecastResponse { WeatherForecasts = weatherForecasts });
        }
    }
}
