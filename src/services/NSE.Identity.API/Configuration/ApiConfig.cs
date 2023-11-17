using NSE.WebAPI.Core.Identity; 
namespace NSE.Identity.API.Configuration;
public static class ApiConfig
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
    {
        services.AddControllers();
        return services;
    }

    public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {

        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthConfiguration();        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        
        return app;
    }
}
