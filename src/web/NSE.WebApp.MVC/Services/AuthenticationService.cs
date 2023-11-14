using NSE.WebApp.MVC.Models;
using System.Text;
using System.Text.Json;

namespace NSE.WebApp.MVC.Services;
public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<string> Login(UserLoginViewModel userLogin)
    {
        var loginContent = new StringContent(
            JsonSerializer.Serialize(userLogin),
            Encoding.UTF8,
            "application/json");
        var response = await _httpClient.PostAsync("https://localhost:7116/api/identity/authenticate", loginContent);
        return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
    }

    public async Task<string> Register(UserRegisterViewModel userRegister)
    {
        var registerContent = new StringContent(
            JsonSerializer.Serialize(userRegister),
            Encoding.UTF8,
            "application/json");
        var response = await _httpClient.PostAsync("https://localhost:7116/api/identity/new-account", registerContent);
        return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
    }
}
