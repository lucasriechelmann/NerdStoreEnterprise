﻿using Microsoft.EntityFrameworkCore;
using NSE.Catalog.API.Data;
using NSE.WebAPI.Core.Identity;

namespace NSE.Catalog.API.Configuration;
public static class ApiConfig
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CatalogContext>(options =>
                   options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddControllers();
        
        services.AddCors(options =>
        {
            options.AddPolicy("Total",
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

        return services;
    }
    public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors("Total");

        app.UseAuthConfiguration();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        return app;
    }
}
