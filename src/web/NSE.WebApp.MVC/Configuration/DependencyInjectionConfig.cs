using Microsoft.AspNetCore.Mvc.DataAnnotations;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Services;
using NSE.WebApp.MVC.Services.Handlers;
using Polly;
using Polly.Extensions.Http;

namespace NSE.WebApp.MVC.Configuration;
public static class DependencyInjectionConfig
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IValidationAttributeAdapterProvider, CpfValidationAttributeAdapterProvider>();
        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
        services.AddHttpClient<IAuthenticationService, AuthenticationService>();

        
        services.AddHttpClient<ICatalogService, CatalogService>()
            //.AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMicroseconds(600)));
            .AddPolicyHandler(PollyExtensions.WaitRetry())
            .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUser, AspNetUser>();

        #region Refit
        //services.AddHttpClient("Refit", options => options.BaseAddress = new Uri(configuration.GetSection("UrlCatalog").Value));
        //    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
        //    .AddTypedClient(Refit.RestService.For<ICatalogoServiceRefit>);
        #endregion
        return services;
    }
}
