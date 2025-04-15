using System.Text.Json.Serialization;

namespace TheWeatherApp.Models
{
    public class Root
    {
        public City city { get; set; } = new();
        public List<WeatherForecast> list { get; set; } = new();
    }

    public class City
    {
        public string name { get; set; } = string.Empty;
        public string country { get; set; } = string.Empty;
    }

    public class WeatherForecast
    {
        public long dt { get; set; }
        public MainWeather main { get; set; } = new();
        public List<WeatherCondition> weather { get; set; } = new();
        public Wind wind { get; set; } = new();
        public string dt_txt { get; set; } = string.Empty;
    }

    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
    }

    public class MainWeather
    {
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int humidity { get; set; }
    }

    public class WeatherCondition
    {
        public string main { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string icon { get; set; } = string.Empty;
    }

    public class CurrentWeatherResponse
    {
        public MainWeather main { get; set; } = new();
        public List<WeatherCondition> weather { get; set; } = new();
        public Wind wind { get; set; } = new();
        public string name { get; set; } = string.Empty;
    }

    public class CityWeather
    {
        public string CityName { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public string Condition { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
    }
}
