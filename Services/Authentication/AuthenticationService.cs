
using AuthApp.Data;
using AuthApp.Models;
using AuthApp_tdsa.Dtos;
using AuthApp_tdsa.Models;
using FirebaseAdmin.Auth;

namespace AuthApp_tdsa.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly DataContext _dataContext;
        public AuthenticationService(HttpClient httpClient, DataContext dataContext)
        {
            _httpClient = httpClient;
            _dataContext = dataContext;
        }

        public async Task<string> LoginAsync(LoginDto request)
        {
            var credentials = new
            {
                request.Email,
                request.Password,
                returnSecureToken = true
            };
            var response = await _httpClient.PostAsJsonAsync("", credentials);

            var authFirebaseObject = await response.Content.ReadFromJsonAsync<AuthFirebase>();

            return authFirebaseObject!.IdToken!;
        }

        public async Task<string> RegisterAsync(RegisterDto request)
        {
            var userArgs = new UserRecordArgs
            {
                Email = request.Email,
                Password = request.Password,
                DisplayName = request.DisplayName
            };
            var usuario = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArgs);


            var userOr = new User
            {
                Uid = usuario.Uid,
                UserName = usuario.DisplayName,
                Email = usuario.Email,
                Password = ""
            };

            await _dataContext.AddAsync(userOr);
            await _dataContext.SaveChangesAsync();

            return usuario.Uid;
        }
    }
}
