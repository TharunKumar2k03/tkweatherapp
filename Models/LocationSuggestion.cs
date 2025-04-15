namespace TheWeatherApp.Models
{
    public class LocationSuggestion
    {
        public string Name { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public double Lat { get; set; } // Added Lat property
        public double Lon { get; set; } // Added Lon property
    }
}