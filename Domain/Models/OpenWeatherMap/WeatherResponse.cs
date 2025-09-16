using System.Text.Json.Serialization;

namespace WiraSystem_Assessment.Domain.Models.OpenWeatherMap
{
    public class WeatherResponse
    {
        [JsonPropertyName("coord")]
        public Coordinates Coord { get; set; }

        [JsonPropertyName("main")]
        public MainData Main { get; set; }

        [JsonPropertyName("wind")]
        public WindData Wind { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Coordinates
    {
        [JsonPropertyName("lon")]
        public double Longitude { get; set; }

        [JsonPropertyName("lat")]
        public double Latitude { get; set; }
    }

    public class MainData
    {
        [JsonPropertyName("temp")]
        public double Temperature { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
    }

    public class WindData
    {
        [JsonPropertyName("speed")]
        public double Speed { get; set; }
    }
}