namespace WiraSystem_Assessment.Application.DTOs
{
    public record WeatherDto(
        double Temperature, // in Celsius
        int Humidity, // in %
        double WindSpeed, // in meters per second
        int AirQualityIndex, // AQI
        double Latitude,
        double Longitude,
        
        // Major pollutants
        double? CO, // Carbon Monoxide
        double? NO, // Nitrogen Monoxide
        double? NO2, // Nitrogen Dioxide
        double? O3, // Ozone
        double? SO2, // Sulphur Dioxide
        double? PM2_5, // Particulates (less than 2.5 μm)
        double? PM10, // Particulates (less than 10 μm)
        double? NH3 // Ammonia
    );
}