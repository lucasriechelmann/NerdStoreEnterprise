using NSE.WebApp.MVC.Extensions;

namespace NSE.WebApp.MVC.Configuration;
public static class MVCConfig
{
    public static IServiceCollection AddMVCConfiguration(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services
            .AddControllersWithViews()
            .AddRazorRuntimeCompilation();

        services.Configure<AppSettings>(configuration);

        return services;
    }
    public static IApplicationBuilder UseMVCConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();            
        }
        else
        {
            app.UseExceptionHandler("/error/500");  
            app.UseStatusCodePagesWithRedirects("/error/{0}");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseIdentityConfiguration();

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });

        return app;
    }
}
