using Microsoft.JSInterop;
using System.Text.Json;

namespace TheWeatherApp.Services
{
    public class CacheService
    {
        private readonly IJSRuntime _jsRuntime;

        public CacheService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task SaveUserState(string path, object state)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "lastPath", path);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "userState", JsonSerializer.Serialize(state));
        }

        public async Task<T?> GetUserState<T>()
        {
            var state = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "userState");
            return string.IsNullOrEmpty(state) ? default : JsonSerializer.Deserialize<T>(state);
        }

        public async Task<bool> HasCachedState()
        {
            var state = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "userState");
            return !string.IsNullOrEmpty(state);
        }

        public async Task ClearCache()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.clear");
        }
    }
} 