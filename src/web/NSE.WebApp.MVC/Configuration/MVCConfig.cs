namespace NSE.WebApp.MVC.Configuration;
public static class MVCConfig
{
    public static IServiceCollection AddMVCConfiguration(this IServiceCollection services)
    {
        services.AddControllersWithViews();

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
            app.UseExceptionHandler("/Home/Error");            
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseIdentityConfiguration();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });

        return app;
    }
}
