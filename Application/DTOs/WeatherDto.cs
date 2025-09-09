namespace WiraSystem_Assessment.Application.DTOs
{
    public class WeatherDto
    {
        public double Temperature { get; set; } // in Celsius
        public int Humidity { get; set; } // in %
        public double WindSpeed { get; set; } // in meters per second
        public int AirQualityIndex { get; set; } // AQI
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        
        // Major pollutants
        public double? CO { get; set; } // Carbon Monoxide
        public double? NO { get; set; } // Nitrogen Monoxide
        public double? NO2 { get; set; } // Nitrogen Dioxide
        public double? O3 { get; set; } // Ozone
        public double? SO2 { get; set; } // Sulphur Dioxide
        public double? PM2_5 { get; set; } // Particulates (less than 2.5 μm)
        public double? PM10 { get; set; } // Particulates (less than 10 μm)
        public double? NH3 { get; set; } // Ammonia
    }
}