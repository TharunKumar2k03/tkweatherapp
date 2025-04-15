using Supabase.Gotrue;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TheWeatherApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly Supabase.Client _client;
        private User? _currentUser;

        public AuthService(Supabase.Client client)
        {
            _client = client;
            _currentUser = _client.Auth.CurrentUser;
        }

        public async Task<User?> GetUser(string userId)
        {
            return await _client.Auth.GetUser(userId);
        }

        public User? GetUser()
        {
            _currentUser = _client.Auth.CurrentUser;
            return _currentUser;
        }

        public Supabase.Client GetSupabaseClient()
        {
            return _client;
        }

        public async Task SignOut()
        {
            await _client.Auth.SignOut();
            _currentUser = null;
            
            if (AuthenticationStateChanged != null)
            {
                await AuthenticationStateChanged.Invoke();
            }
        }

        public async Task<Session?> SignUp(string email, string password)
        {
            return await _client.Auth.SignUp(email, password);
        }

        public async Task<Session?> SignUpAsync(string email, string password, string firstName = "", string lastName = "")
        {
            var userMetadata = new Dictionary<string, object>
            {
                { "first_name", firstName },
                { "last_name", lastName }
            };

            return await _client.Auth.SignUp(email, password, new SignUpOptions 
            { 
                Data = userMetadata 
            });
        }

        public async Task<Session?> SignInAsync(string email, string password)
        {
            return await _client.Auth.SignIn(email, password);
        }

        public async Task<Session?> SignIn(string email, string password)
        {
            var session = await _client.Auth.SignIn(email, password);
            _currentUser = _client.Auth.CurrentUser;
            
            if (AuthenticationStateChanged != null)
            {
                await AuthenticationStateChanged.Invoke();
            }
            
            return session;
        }

        public async Task<bool> SignIn(string email, SignInOptions options)
        {
            await _client.Auth.SignIn(email, options);
            return true;
        }

        public async Task<UserDetails?> GetUserDetailsAsync()
        {
            var user = _currentUser;

            if (user != null)
            {
                return new UserDetails
                {
                    FirstName = user.UserMetadata?.GetValueOrDefault("first_name")?.ToString() ?? "",
                    LastName = user.UserMetadata?.GetValueOrDefault("last_name")?.ToString() ?? "",
                    Email = user.Email
                };
            }

            return null;
        }

        public event Func<Task>? AuthenticationStateChanged;
    }

    public class UserDetails
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}
