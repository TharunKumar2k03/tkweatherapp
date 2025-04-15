using Supabase.Gotrue;
using System;
using System.Threading.Tasks;

namespace TheWeatherApp.Services
{
    public interface IAuthService
    {
        event Func<Task>? AuthenticationStateChanged;
        Task<User?> GetUser(string userId);
        User? GetUser();
        Supabase.Client GetSupabaseClient();
        Task SignOut();
        Task<Session?> SignUp(string email, string password);
        Task<Session?> SignUpAsync(string email, string password, string firstName = "", string lastName = "");
        Task<Session?> SignInAsync(string email, string password);
        Task<Session?> SignIn(string email, string password);
        Task<bool> SignIn(string email, SignInOptions options);
        Task<UserDetails?> GetUserDetailsAsync();
    }
}
