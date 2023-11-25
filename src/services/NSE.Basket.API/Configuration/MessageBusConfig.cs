using NSE.Basket.API.Services;
using NSE.Core.Extensions;
using NSE.MessageBus;
namespace NSE.Basket.API.Configuration;
public static class MessageBusConfig
{
    public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
    {
        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
            .AddHostedService<BasketIntegrationHandler>();
    }
}
