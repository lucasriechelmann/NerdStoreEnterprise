using MediatR;
using NSE.Core.Mediator;
using NSE.Customer.API.Data;
using NSE.Customer.API.Data.Repository;
using NSE.Customer.API.Models;

namespace NSE.Customer.API.Configuration;
public static class DependencyInjectionConfig
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        //services.AddScoped<IMediatorHandler, MediatorHandler>();
        //services.AddScoped<IRequestHandler<RegistrarClienteCommand, ValidationResult>, ClienteCommandHandler>();

        //services.AddScoped<INotificationHandler<ClienteRegistradoEvent>, ClienteEventHandler>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<CustomerContext>();
        return services;
    }
}
