using System.Threading.Tasks;
using WiraSystem_Assessment.Domain.Models.OpenWeatherMap;

namespace WiraSystem_Assessment.Application.Interfaces
{
    public interface IOpenWeatherMapClient
    {
        /// <summary>
        /// Gets current weather data for a given city from OpenWeatherMap API.
        /// </summary>
        /// <param name="cityName">The name of the city</param>
        /// <returns>Weather response data</returns>
        Task<WeatherResponse> GetWeatherDataAsync(string cityName);

        /// <summary>
        /// Gets current air pollution data for given coordinates from OpenWeatherMap API.
        /// </summary>
        /// <param name="latitude">Latitude coordinate</param>
        /// <param name="longitude">Longitude coordinate</param>
        /// <returns>Air pollution response data</returns>
        Task<AirPollutionResponse> GetAirPollutionDataAsync(double latitude, double longitude);
    }
}