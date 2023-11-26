using NSE.Core.Extensions;
using NSE.MessageBus;

namespace NSE.Order.API.Configuration;
public static class MessageBusConfig
{
    public static IServiceCollection AddMessageBusConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));

        return services;
    }
}
