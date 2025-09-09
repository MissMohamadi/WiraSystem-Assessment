using System.Net.Http.Json;
using System.Text.Json;
using WiraSystem_Assessment.Application.Interfaces;
using WiraSystem_Assessment.Domain.Models.OpenWeatherMap;

namespace WiraSystem_Assessment.Infrastructure.ExternalClients
{
    public class OpenWeatherMapClient : IOpenWeatherMapClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenWeatherMapClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["OpenWeatherMapApiKey"] 
                ?? throw new InvalidOperationException("OpenWeatherMap API key is not configured.");
            
            // تنظیم BaseAddress برای HttpClient
            _httpClient.BaseAddress = new Uri("https://api.openweathermap.org/");
        }

        public async Task<WeatherResponse> GetWeatherDataAsync(string cityName)
        {
            var url = $"data/2.5/weather?q={cityName}&appid={_apiKey}&units=metric";
            
            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                
                var weatherResponse = await response.Content.ReadFromJsonAsync<WeatherResponse>();
                return weatherResponse ?? throw new InvalidOperationException("Failed to deserialize weather data.");
            }
            catch (HttpRequestException ex)
            {
                // Log the exception (در یک برنامه واقعی از یک logging framework استفاده می‌کنیم)
                throw new ApplicationException($"Error fetching weather data for city '{cityName}': {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new ApplicationException($"Error deserializing weather data for city '{cityName}': {ex.Message}", ex);
            }
        }

        public async Task<AirPollutionResponse> GetAirPollutionDataAsync(double latitude, double longitude)
        {
            var url = $"data/2.5/air_pollution?lat={latitude}&lon={longitude}&appid={_apiKey}";
            
            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                
                var airPollutionResponse = await response.Content.ReadFromJsonAsync<AirPollutionResponse>();
                return airPollutionResponse ?? throw new InvalidOperationException("Failed to deserialize air pollution data.");
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException($"Error fetching air pollution data for coordinates ({latitude}, {longitude}): {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new ApplicationException($"Error deserializing air pollution data for coordinates ({latitude}, {longitude}): {ex.Message}", ex);
            }
        }
    }
}