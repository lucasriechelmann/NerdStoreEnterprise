using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using NSE.WebApp.MVC.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace NSE.WebApp.MVC.Services;
public class AuthenticationService : Service, IAuthenticationService
{
    private readonly HttpClient _httpClient;
    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<UserLoginResponse> Login(UserLoginViewModel userLogin)
    {
        var loginContent = new StringContent(
            JsonSerializer.Serialize(userLogin),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync("https://localhost:7116/api/identity/authenticate", loginContent);

        if (!HandleResponseError(response))
        {
            return new UserLoginResponse()
            {
                ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStreamAsync(), GetOptions())
            };
        }

        return JsonSerializer.Deserialize<UserLoginResponse>(await response.Content.ReadAsStringAsync(), GetOptions());
    }

    public async Task<UserLoginResponse> Register(UserRegisterViewModel userRegister)
    {
        var registerContent = new StringContent(
            JsonSerializer.Serialize(userRegister),
            Encoding.UTF8,
            "application/json");
        
        var response = await _httpClient.PostAsync("https://localhost:7116/api/identity/new-account", registerContent);

        if (!HandleResponseError(response))
        {
            return new UserLoginResponse()
            {
                ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStreamAsync(), GetOptions())
            };
        }

        return JsonSerializer.Deserialize<UserLoginResponse>(await response.Content.ReadAsStringAsync(), GetOptions());
    }
    JsonSerializerOptions GetOptions() =>
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
}
