using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace NSE.WebApp.MVC.Services;
public class AuthenticationService : Service, IAuthenticationService
{
    private readonly HttpClient _httpClient;
    public AuthenticationService(HttpClient httpClient, IOptions<AppSettings> settings)
    {        
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.UrlAuthentication);
    }
    public async Task<UserLoginResponse> Login(UserLoginViewModel userLogin)
    {
        var loginContent = GetContent(userLogin);

        var response = await _httpClient.PostAsync("/api/identity/authenticate", loginContent);

        if (!HandleResponseError(response))
        {
            return new UserLoginResponse()
            {
                ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
            };
        }

        return await DeserializeObjectResponse<UserLoginResponse>(response);
    }

    public async Task<UserLoginResponse> Register(UserRegisterViewModel userRegister)
    {
        var registerContent = GetContent(userRegister);
        
        var response = await _httpClient.PostAsync("/api/identity/new-account", registerContent);

        if (!HandleResponseError(response))
        {
            return new UserLoginResponse()
            {
                ResponseResult = await DeserializeObjectResponse<ResponseResult>(response)
            };
        }

        return await DeserializeObjectResponse<UserLoginResponse>(response);
    }
    
}
