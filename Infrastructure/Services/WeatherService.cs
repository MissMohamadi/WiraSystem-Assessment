using WiraSystem_Assessment.Application.Interfaces;
using WiraSystem_Assessment.Application.DTOs;

namespace WiraSystem_Assessment.Infrastructure.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IOpenWeatherMapClient _openWeatherMapClient;

        public WeatherService(IOpenWeatherMapClient openWeatherMapClient)
        {
            _openWeatherMapClient = openWeatherMapClient ?? throw new ArgumentNullException(nameof(openWeatherMapClient));
        }

        public async Task<WeatherDto> GetWeatherAsync(string cityName)
        {
            // اعتبارسنجی ورودی
            if (string.IsNullOrWhiteSpace(cityName))
                throw new ArgumentException("City name cannot be null or empty", nameof(cityName));

            try
            {
                // 1. گرفتن داده‌های آب و هوا
                var weatherData = await _openWeatherMapClient.GetWeatherDataAsync(cityName);
                
                if (weatherData?.Main == null || weatherData?.Coord == null)
                    throw new InvalidOperationException("Invalid weather data received from API");

                // 2. گرفتن داده‌های کیفیت هوا با استفاده از مختصات
                var airPollutionData = await _openWeatherMapClient.GetAirPollutionDataAsync(
                    weatherData.Coord.Latitude,
                    weatherData.Coord.Longitude
                );

                // 3. گرفتن اولین آیتم داده‌های کیفیت هوا
                var airData = airPollutionData?.List?.FirstOrDefault();

                // 4. ایجاد WeatherDto با استفاده از record constructor
                return new WeatherDto(
                    Temperature: weatherData.Main.Temperature,
                    Humidity: weatherData.Main.Humidity,
                    WindSpeed: weatherData.Wind?.Speed ?? 0,
                    AirQualityIndex: airData?.Main?.Aqi ?? 0,
                    Latitude: weatherData.Coord.Latitude,
                    Longitude: weatherData.Coord.Longitude,
                    CO: airData?.Components?.Co,
                    NO: airData?.Components?.No,
                    NO2: airData?.Components?.No2,
                    O3: airData?.Components?.O3,
                    SO2: airData?.Components?.So2,
                    PM2_5: airData?.Components?.Pm2_5,
                    PM10: airData?.Components?.Pm10,
                    NH3: airData?.Components?.Nh3
                );
            }
            catch (Exception ex)
            {
                // لاگ کردن خطا برای دیباگ
                throw new ApplicationException($"Error fetching weather data for city: {cityName}", ex);
            }
        }
    }
}