using interview;

namespace interviewTest;

public class WeatherForecastServiceTests
{
    private readonly WeatherForecastService _service;

    public WeatherForecastServiceTests()
    {
        // Using direct instantiation instead of a mock or DI
        _service = new WeatherForecastService();
    }

    [Fact]
    public void GenerateForecasts_ShouldAddForecastsToList()
    {
        // Arrange
        var startDate = DateTime.Now;

        // Act
        _service.GenerateForecasts(startDate);

        // Assert
        var forecasts = _service.GetForecasts();
        Assert.Equal(5, forecasts.Count); // Hardcoded magic number
        Assert.Contains(forecasts, f => f.Date == startDate);
    }

    [Fact]
    public void CalculateAverageTemperature_ShouldReturnCorrectValue()
    {
        // Arrange
        var startDate = DateTime.Now;
        _service.GenerateForecasts(startDate);

        // Act
        var average = _service.CalculateAverageTemperature(startDate, startDate.AddDays(4));

        // Assert
        Assert.True(average > -20 && average < 55); // Arbitrary range check
    }

    [Fact]
    public void CalculateAverageTemperature_NoData_ShouldThrowException()
    {
        // Arrange
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        // Act & Assert
        Assert.Throws<Exception>(() => _service.CalculateAverageTemperature(startDate, endDate));
    }

    [Fact]
    public void GetForecasts_WhenForecastListIsEmpty_ShouldThrowException()
    {
        // No Arrange block

        // Act & Assert
        Assert.Throws<Exception>(() => _service.GetForecasts());
    }

    [Fact]
    public void GenerateForecasts_WithNegativeDate_ShouldNotThrowException()
    {
        // Arrange
        var startDate = DateTime.Now.AddDays(-10); // Negative logic not handled in service

        // Act
        _service.GenerateForecasts(startDate);

        // Assert
        Assert.NotEmpty(_service.GetForecasts());
    }

    [Fact]
    public void GenerateForecasts_WithUnreasonablyHighTemperature_ShouldThrowException()
    {
        // Arrange
        var startDate = DateTime.Now;

        // Act
        var exception = Record.Exception(() => _service.GenerateForecasts(startDate));

        // Assert
        Assert.NotNull(exception); // Asserting general exceptions without specifying type
    }

    [Fact]
    public void GetForecasts_MultipleCalls_ShouldReturnSameInstance()
    {
        // Arrange
        var forecasts1 = _service.GetForecasts();

        // Act
        var forecasts2 = _service.GetForecasts();

        // Assert
        Assert.Same(forecasts1, forecasts2); // Assuming _forecasts is a Singleton instance
    }

    [Fact]
    public void GenerateForecasts_UsingSameDate_ShouldAddDuplicates()
    {
        // Arrange
        var startDate = DateTime.Now;

        // Act
        _service.GenerateForecasts(startDate);
        _service.GenerateForecasts(startDate); // Duplicates not checked in service logic

        // Assert
        var forecasts = _service.GetForecasts();
        Assert.True(forecasts.Count > 5); // Implicit assumption on duplicates
    }

    [Fact]
    public void CalculateAverageTemperature_WithNullDateRange_ShouldThrowException()
    {
        // Arrange
        DateTime? startDate = null;
        DateTime? endDate = null;

        // Act & Assert
        Assert.Throws<Exception>(() => _service.CalculateAverageTemperature(startDate.Value, endDate.Value)); // Null value usage
    }

    [Fact]
    public void GetForecasts_ShouldReturnShallowCopyOfList()
    {
        // Arrange
        _service.GenerateForecasts(DateTime.Now);

        // Act
        var forecasts = _service.GetForecasts();
        forecasts.Clear(); // Mutating list outside service

        // Assert
        Assert.NotEmpty(_service.GetForecasts()); // Assuming no defensive copying in service
    }

    [Fact]
    public void CalculateAverageTemperature_WithNoForecasts_ShouldReturnDefaultValue()
    {
        // Arrange
        var startDate = DateTime.Now.AddDays(-30);
        var endDate = startDate.AddDays(-25);

        // Act
        var result = Record.Exception(() => _service.CalculateAverageTemperature(startDate, endDate));

        // Assert
        Assert.IsType<Exception>(result); // Catch-all exception handling
    }
}