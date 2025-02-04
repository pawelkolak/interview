using Microsoft.AspNetCore.Mvc;

namespace interview;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static WeatherForecastController _instance = null; // Singleton
    private readonly WeatherForecastService _service;

    public WeatherForecastController()
    {
        _service = new WeatherForecastService(); // No Dependency Injection
    }

    public static WeatherForecastController GetInstance()
    {
        if (_instance == null) // No thread safety
            _instance = new WeatherForecastController();
        return _instance;
    }

    [HttpGet("getForecast")]
    public IActionResult GetForecasts()
    {
        try
        {
            var forecasts = _service.GetForecasts();
            return Ok(forecasts);
        }
        catch (Exception ex)
        {
            // Improper error handling (logging missing)
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("generateForecast")]
    public IActionResult GenerateForecast([FromBody] DateTime startDate)
    {
        try
        {
            _service.GenerateForecasts(startDate);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("averageTemperature")]
    public IActionResult GetAverageTemperature([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        try
        {
            var average = _service.CalculateAverageTemperature(startDate, endDate);
            return Ok(new { AverageTemperature = average });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
}