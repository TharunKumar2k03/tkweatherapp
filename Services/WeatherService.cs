using System.Net.Http.Json;
using TheWeatherApp.Models;
using Microsoft.Extensions.Configuration;

namespace TheWeatherApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public WeatherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _apiKey = "902a7076f0a33886f94bd38cc08b4f24";
            _baseUrl = "https://api.openweathermap.org";
        }

        // Fetch location suggestions for autocomplete
        public async Task<List<LocationSuggestion>> GetLocationSuggestionsAsync(string query)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/geo/1.0/direct?q={query}&limit=5&appid={_apiKey}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<LocationSuggestion>>() ?? new List<LocationSuggestion>();
                }
                return new List<LocationSuggestion>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching location suggestions: {ex.Message}");
            }
        }
    }
}
