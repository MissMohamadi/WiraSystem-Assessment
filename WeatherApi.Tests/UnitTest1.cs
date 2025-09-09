using Xunit;
using Moq;
using WiraSystem_Assessment.Infrastructure.Services;
using WiraSystem_Assessment.Application.Interfaces;
using System.Threading.Tasks;

namespace WeatherApi.Tests;

public class WeatherServiceTests
{
    [Fact]
    public async Task GetWeatherAsync_Should_Return_Null_When_City_Not_Found()
    {
        // Arrange
        var mockOpenWeatherClient = new Mock<IOpenWeatherMapClient>();
        
        // وقتی شهر پیدا نشه، null برمی‌گردونه
        mockOpenWeatherClient
            .Setup(x => x.GetWeatherDataAsync("InvalidCityName12345"))
            .ReturnsAsync((WeatherData)null); // یا throws مناسب

        var service = new WeatherService(mockOpenWeatherClient.Object);

        // Act
        var result = await service.GetWeatherAsync("InvalidCityName12345");

        // Assert
        Assert.Null(result);
    }
}