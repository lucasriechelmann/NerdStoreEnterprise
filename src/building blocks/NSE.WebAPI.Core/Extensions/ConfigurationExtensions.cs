using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NSE.WebAPI.Core.Extensions;
public static class ConfigurationExtensions
{
    public static IConfiguration AddAppSettingsJsonConfiguration(this ConfigurationManager configuration, IWebHostEnvironment env)
    {
        configuration
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables();
        return configuration;
    }
}
