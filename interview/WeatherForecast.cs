namespace interview;

public class WeatherForecast
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }

    // Poor naming convention
    public string summary { get; set; }

    // No validation for calculations
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}