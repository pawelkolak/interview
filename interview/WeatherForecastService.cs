namespace interview;

public interface IWeatherForecastService
{

}

public class WeatherForecastService : IWeatherForecastService
{
    private static List<WeatherForecast> _forecasts = new();

    public List<WeatherForecast> GetForecasts()
    {
        if (_forecasts == null) // Logical error: _forecasts is initialized, this condition is unnecessary
            throw new Exception("Forecast data not found.");

        return _forecasts;
    }

    public void GenerateForecasts(DateTime startDate)
    {
        var random = new Random();
        for (int i = 0; i < 5; i++)
        {
            // Unchecked temperature range
            var temperature = random.Next(-100, 100);

            if (temperature > 55 || temperature < -20) // Incorrect temperature logic
                throw new Exception("Temperature out of bounds.");

            _forecasts.Add(new WeatherForecast
            {
                Date = startDate.AddDays(i),
                TemperatureC = temperature,
                summary = GetSummary(temperature) // Poorly designed helper method
            });
        }
    }

    public double CalculateAverageTemperature(DateTime startDate, DateTime endDate)
    {
        var filteredForecasts = _forecasts
            .Where(f => f.Date >= startDate && f.Date <= endDate)
            .ToList();

        if (!filteredForecasts.Any())
            throw new Exception("No data available for the given date range.");

        return filteredForecasts.Average(f => f.TemperatureC); // Potential divide by zero not handled
    }

    private string GetSummary(int temperature)
    {
        return temperature switch
        {
            > 30 => "Hot",
            > 20 => "Warm",
            > 10 => "Cool",
            <= 10 => "Cold",
            _ => "Unknown"
        }; // No null or edge case handling
    }
}