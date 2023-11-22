using NSE.Basket.API.Data;
using NSE.WebAPI.Core.User;

namespace NSE.Basket.API.Configuration;
public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IAspNetUser, AspNetUser>();
        services.AddScoped<BasketContext>();
    }
}
