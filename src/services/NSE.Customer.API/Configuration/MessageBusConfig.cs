using NSE.Core.Extensions;
using NSE.Customer.API.Services;
using NSE.MessageBus;

namespace NSE.Customer.API.Configuration
{
    public static class MessageBusConfig
    {
        public static IServiceCollection AddMessageBusConfiguration(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services
                .AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<CustomerRegisterIntegrationHandler>();
            return services;
        }
    }
}
