using Microsoft.AspNetCore.Components.Authorization;
using Supabase.Gotrue;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TheWeatherApp.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly AuthService _authService;

        public CustomAuthStateProvider(AuthService authService)
        {
            _authService = authService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = _authService.GetUser();
            if (user == null)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Email ?? ""),
                new Claim(ClaimTypes.NameIdentifier, user.Id ?? ""),
            }, "SupabaseAuth");

            var userPrincipal = new ClaimsPrincipal(identity);
            return new AuthenticationState(userPrincipal);
        }

        public new void NotifyAuthenticationStateChanged(Task<AuthenticationState> authState)
        {
            base.NotifyAuthenticationStateChanged(authState);
        }
    }
}
