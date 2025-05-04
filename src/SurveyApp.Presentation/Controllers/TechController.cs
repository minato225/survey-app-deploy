using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SurveyApp.Presentation.Models;

namespace SurveyApp.Presentation.Controllers;

[ApiController]
[Route("api/weatherforecast")]
public class TechController(
    ILogger<TechController> logger,
    IHttpContextAccessor httpContextAccessor)
    : ControllerBase
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    [HttpGet]
    [Authorize]
    public IEnumerable<WeatherForecast> Get()
    {
        var refreshToken = Request.Cookies["RefreshToken"];
        var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpGet]
    public IEnumerable<int> GetDump() => [1, 2, 3];
}