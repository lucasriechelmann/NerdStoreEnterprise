using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;
public interface IAuthenticationService
{
    Task<UserLoginResponse> Login(UserLoginViewModel userLogin);
    Task<UserLoginResponse> Register(UserRegisterViewModel userRegister);
}
