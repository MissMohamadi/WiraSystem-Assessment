using WiraSystem_Assessment.Application.Interfaces;
using WiraSystem_Assessment.Application.DTOs;

namespace WiraSystem_Assessment.Infrastructure.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IOpenWeatherMapClient _openWeatherMapClient;

        public WeatherService(IOpenWeatherMapClient openWeatherMapClient)
        {
            _openWeatherMapClient = openWeatherMapClient;
        }

        public async Task<WeatherDto> GetWeatherAsync(string cityName)
        {
            // 1. گرفتن داده‌های آب و هوا
            var weatherData = await _openWeatherMapClient.GetWeatherDataAsync(cityName);

            // 2. گرفتن داده‌های کیفیت هوا با استفاده از مختصات
            var airPollutionData = await _openWeatherMapClient.GetAirPollutionDataAsync(
                weatherData.Coord.Latitude,
                weatherData.Coord.Longitude
            );

            // 3. ترکیب داده‌ها و ایجاد WeatherDto
            var weatherDto = new WeatherDto
            {
                Temperature = weatherData.Main.Temperature,
                Humidity = weatherData.Main.Humidity,
                WindSpeed = weatherData.Wind.Speed,
                Latitude = weatherData.Coord.Latitude,
                Longitude = weatherData.Coord.Longitude,
                // AQI و آلاینده‌ها از اولین آیتم لیست گرفته می‌شن
                AirQualityIndex = airPollutionData.List?.FirstOrDefault()?.Main?.Aqi ?? 0,
                CO = airPollutionData.List?.FirstOrDefault()?.Components?.Co,
                NO = airPollutionData.List?.FirstOrDefault()?.Components?.No,
                NO2 = airPollutionData.List?.FirstOrDefault()?.Components?.No2,
                O3 = airPollutionData.List?.FirstOrDefault()?.Components?.O3,
                SO2 = airPollutionData.List?.FirstOrDefault()?.Components?.So2,
                PM2_5 = airPollutionData.List?.FirstOrDefault()?.Components?.Pm2_5,
                PM10 = airPollutionData.List?.FirstOrDefault()?.Components?.Pm10,
                NH3 = airPollutionData.List?.FirstOrDefault()?.Components?.Nh3
            };

            return weatherDto;
        }
    }
}