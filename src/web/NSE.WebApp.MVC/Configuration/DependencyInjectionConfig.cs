using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Configuration;
public static class DependencyInjectionConfig
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddHttpClient<IAuthenticationService, AuthenticationService>();

        return services;
    }
}
