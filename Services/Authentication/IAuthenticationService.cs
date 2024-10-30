using AuthApp_tdsa.Dtos;

namespace AuthApp_tdsa.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<string> RegisterAsync(RegisterDto request);
        Task<string> LoginAsync(LoginDto request);
    }
}
