using Xunit;
using Moq;
using Moq.Protected;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using WeatherApi.Services; // این خط مهم است

namespace WeatherApi.Tests;

public class WeatherServiceTests
{
    [Fact]
    public async Task GetWeatherAsync_Should_Return_Null_When_City_Not_Found()
    {
        // Arrange
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        
        // Setup mock HTTP response
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("")
            });

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        
        var inMemorySettings = new Dictionary<string, string> {
            {"OpenWeatherMap:ApiKey", "fake-key"},
            {"OpenWeatherMap:BaseUrl", "https://api.openweathermap.org/data/2.5/"}
        };
        
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var service = new WeatherService(httpClient, configuration);

        // Act
        var result = await service.GetWeatherAsync("InvalidCityName12345");

        // Assert
        Assert.Null(result);
    }
}