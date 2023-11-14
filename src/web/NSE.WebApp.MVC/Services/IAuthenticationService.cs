using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;
public interface IAuthenticationService
{
    Task<string> Login(UserLoginViewModel userLogin);
    Task<string> Register(UserRegisterViewModel userRegister);
}
