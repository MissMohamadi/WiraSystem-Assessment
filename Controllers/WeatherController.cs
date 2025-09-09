using Microsoft.AspNetCore.Mvc;
using WiraSystem_Assessment.Application.Interfaces;
using WiraSystem_Assessment.Application.DTOs;

namespace WiraSystem_Assessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        /// <summary>
        /// Gets current weather and air quality information for a given city.
        /// </summary>
        /// <param name="city">The name of the city</param>
        /// <returns>Weather and air quality data</returns>
        [HttpGet("{city}")]
        public async Task<ActionResult<WeatherDto>> GetWeather(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return BadRequest("City name is required.");
            }

            try
            {
                var weatherData = await _weatherService.GetWeatherAsync(city);
                return Ok(weatherData);
            }
            catch (Exception ex)
{
    // Log the exception (in a real app, use a logging framework)
    // حالا ارور واقعی رو نشون می‌ده
    return StatusCode(500, $"An error occurred while fetching weather data: {ex.Message}");
}

        }
    }
}