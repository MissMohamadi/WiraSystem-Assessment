using System.Threading.Tasks;
using WiraSystem_Assessment.Application.DTOs;

namespace WiraSystem_Assessment.Application.Interfaces
{
    public interface IWeatherService
    {
        /// <summary>
        /// Gets current weather and air quality information for a given city.
        /// </summary>
        /// <param name="cityName">The name of the city</param>
        /// <returns>A DTO containing weather and air quality data</returns>
        Task<WeatherDto> GetWeatherAsync(string cityName);
    }
}