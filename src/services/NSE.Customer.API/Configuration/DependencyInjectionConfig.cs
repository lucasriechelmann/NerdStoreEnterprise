using FluentValidation.Results;
using MediatR;
using NSE.Core.Mediator;
using NSE.Customer.API.Application.Commands;
using NSE.Customer.API.Application.Events;
using NSE.Customer.API.Data;
using NSE.Customer.API.Data.Repository;
using NSE.Customer.API.Models;

namespace NSE.Customer.API.Configuration;
public static class DependencyInjectionConfig
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IRequestHandler<CustomerRegisterCommand, ValidationResult>, CustomerCommandHandler>();

        services.AddScoped<INotificationHandler<CustomerRegisteredEvent>, CustomerEventHandler>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<CustomerContext>();
        return services;
    }
}
