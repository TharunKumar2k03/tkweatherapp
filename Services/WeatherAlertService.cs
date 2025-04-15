using System.Net.Mail;
using System.Net;
using MudBlazor;
using System.Net.Http.Json;
using Supabase.Gotrue;
using Microsoft.JSInterop;

namespace TheWeatherApp.Services
{
    public class WeatherAlertService
    {
        private readonly HttpClient _httpClient;
        private readonly ISnackbar _snackbar;
        private readonly AuthService _authService;
        private readonly IJSRuntime _jsRuntime;
        private Timer? _checkWeatherTimer;
        private const double MAX_TEMP_THRESHOLD = 30.0;
        private const double MIN_TEMP_THRESHOLD = 10.0;

        public WeatherAlertService(HttpClient httpClient, ISnackbar snackbar, AuthService authService, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _snackbar = snackbar;
            _authService = authService;
            _jsRuntime = jsRuntime;
        }

        public void StartWeatherMonitoring(string apiKey, List<string> favoriteCities)
        {

            _checkWeatherTimer?.Dispose();


            _checkWeatherTimer = new Timer(async _ => await CheckWeatherConditions(apiKey, favoriteCities), null, 0, 60000);
        }

        public void StopWeatherMonitoring()
        {
            _checkWeatherTimer?.Dispose();
            _checkWeatherTimer = null;
        }

        private async Task CheckWeatherConditions(string apiKey, List<string> favoriteCities)
        {
            try
            {
                var user = _authService.GetUser();
                if (user == null) return;

                // Check current location weather
                var locationResult = await GetCurrentLocation();
                if (locationResult != null)
                {
                    await CheckLocationWeather(locationResult.Latitude, locationResult.Longitude, "Current Location", apiKey, user.Email);
                }

                // Check favorite cities weather
                foreach (var city in favoriteCities)
                {
                    var response = await _httpClient.GetFromJsonAsync<CurrentWeatherResponse>(
                        $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric");

                    if (response != null)
                    {
                        await CheckTemperature(response.main.temp, city, user.Email);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking weather conditions: {ex.Message}");
            }
        }

        private async Task CheckLocationWeather(double lat, double lon, string locationName, string apiKey, string userEmail)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<CurrentWeatherResponse>(
                    $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units=metric");

                if (response != null)
                {
                    await CheckTemperature(response.main.temp, locationName, userEmail);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking location weather: {ex.Message}");
            }
        }

        private async Task CheckTemperature(double temperature, string location, string userEmail)
        {
            if (temperature > MAX_TEMP_THRESHOLD)
            {
                await ShowInAppNotification($"High temperature alert in {location}: {temperature:F1}°C");
            }
            else if (temperature < MIN_TEMP_THRESHOLD)
            {
                await ShowInAppNotification($"Low temperature alert in {location}: {temperature:F1}°C");
            }
        }

        private async Task ShowInAppNotification(string message)
        {
            await Task.Run(() => _snackbar.Add(message, Severity.Warning, config =>
            {
                config.ShowCloseIcon = true;
                config.VisibleStateDuration = 5000;
            }));
        }

        private async Task<LocationResult?> GetCurrentLocation()
        {
            try
            {
                // This assumes you have the getLocationAsync JavaScript function
                return await _jsRuntime.InvokeAsync<LocationResult>("getLocationAsync");
            }
            catch
            {
                return null;
            }
        }

        private class LocationResult
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }

        private class CurrentWeatherResponse
        {
            public MainData main { get; set; } = new MainData();
            public string name { get; set; } = string.Empty;
            public List<WeatherData> weather { get; set; } = new List<WeatherData>();
        }

        private class MainData
        {
            public double temp { get; set; }
        }

        private class WeatherData
        {
            public string description { get; set; } = string.Empty;
        }
    }
}
