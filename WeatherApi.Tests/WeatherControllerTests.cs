using Moq;
using WiraSystem_Assessment.Controllers;
using WiraSystem_Assessment.Application.Interfaces;
using WiraSystem_Assessment.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace WeatherApi.Tests
{
    public class WeatherControllerTests
    {
        private readonly Mock<IWeatherService> _mockWeatherService;
        private readonly WeatherController _controller;

        public WeatherControllerTests()
        {
            _mockWeatherService = new Mock<IWeatherService>();
            _controller = new WeatherController(_mockWeatherService.Object);
        }

        [Fact]
        public async Task GetWeather_ReturnsBadRequest_WhenCityIsNullOrWhiteSpace()
        {
            // Arrange
            string invalidCity = "   ";

            // Act
            var result = await _controller.GetWeather(invalidCity);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("City name is required.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetWeather_ReturnsOk_WithWeatherData_WhenServiceReturnsData()
        {
            // Arrange
            var fakeWeatherData = new WeatherDto(
                City: "Rasht",
                Temperature: 25.5,
                Humidity: 60,
                WindSpeed: 5.5,
                AirQualityIndex: 42,
                Latitude: 37.28,
                Longitude: 49.58,
                CO: 0.5,
                NO: null,
                NO2: 20.0,
                O3: 50.0,
                SO2: 5.0,
                PM2_5: 15.0,
                PM10: 30.0,
                NH3: 10.0
            );

            _mockWeatherService
                .Setup(x => x.GetWeatherAsync("Rasht"))
                .ReturnsAsync(fakeWeatherData);

            // Act
            var result = await _controller.GetWeather("Rasht");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedData = Assert.IsType<WeatherDto>(okResult.Value);

            Assert.Equal("Rasht", returnedData.City);
            Assert.Equal(25.5, returnedData.Temperature);
            Assert.Equal(42, returnedData.AirQualityIndex);
        }

        [Fact]
        public async Task GetWeather_ReturnsStatusCode500_WhenServiceThrowsException()
        {
            // Arrange
            _mockWeatherService
                .Setup(x => x.GetWeatherAsync("Tehran"))
                .ThrowsAsync(new System.Exception("API is down"));

            // Act
            var result = await _controller.GetWeather("Tehran");

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Contains("An error occurred while fetching weather data: API is down", statusCodeResult.Value.ToString());
        }
    }
}