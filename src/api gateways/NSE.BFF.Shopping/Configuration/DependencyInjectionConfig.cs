using NSE.BFF.Shopping.Extensions;
using NSE.BFF.Shopping.Services;
using NSE.WebAPI.Core.Extensions;
using NSE.WebAPI.Core.User;
using Polly;

namespace NSE.BFF.Shopping.Configuration;
public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IAspNetUser, AspNetUser>();

        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<ICatalogService, CatalogService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddPolicyHandler(PollyExtensions.WaitRetry())
                .AddTransientHttpErrorPolicy(
                    p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        services.AddHttpClient<IBasketService, BasketService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            .AddPolicyHandler(PollyExtensions.WaitRetry())
            .AddTransientHttpErrorPolicy(
                p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        services.AddHttpClient<IOrderService, OrderService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            .AddPolicyHandler(PollyExtensions.WaitRetry())
            .AddTransientHttpErrorPolicy(
                p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        services.AddHttpClient<ICustomerService, CustomerService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            .AddPolicyHandler(PollyExtensions.WaitRetry())
            .AddTransientHttpErrorPolicy(
                p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
    }
}
