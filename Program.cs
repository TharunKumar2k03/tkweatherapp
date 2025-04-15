using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TheWeatherApp;
using TheWeatherApp.Services;
using MudBlazor;
using MudBlazor.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Supabase;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://tkweatherapp-backend-production.up.railway.app/") });

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = true;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 3000;
    
    config.PopoverOptions.ThrowOnDuplicateProvider = false;
});

var supabaseUrl = "https://pplgrjyiywqfvdajczbb.supabase.co";
var supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InBwbGdyanlpeXdxZnZkYWpjemJiIiwicm9sZSI6ImFub24iLCJpYXQiOjE3Mzc5OTQyMDcsImV4cCI6MjA1MzU3MDIwN30.ZY20SznS-y-hCC0YcMgURpoyJk-mM8aQUOjx4pf4wCM";
var options = new SupabaseOptions
{
    AutoRefreshToken = true,
    AutoConnectRealtime = true
};

builder.Services.AddScoped<Supabase.Client>(_ => 
    new Supabase.Client(supabaseUrl, supabaseKey, options));

// Register Authentication Services
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Add CacheService registration
builder.Services.AddScoped<CacheService>();

// Register WeatherAlertService
builder.Services.AddScoped<WeatherAlertService>();

await builder.Build().RunAsync();
