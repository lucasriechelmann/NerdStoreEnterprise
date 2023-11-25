using FluentValidation.Results;
using MediatR;
using NSE.Core.Mediator;
using NSE.Customer.API.Application.Commands;
using NSE.Customer.API.Application.Events;
using NSE.Customer.API.Data;
using NSE.Customer.API.Data.Repository;
using NSE.Customer.API.Models;
using NSE.Customer.API.Services;
using NSE.WebAPI.Core.User;

namespace NSE.Customer.API.Configuration;
public static class DependencyInjectionConfig
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IAspNetUser, AspNetUser>();

        services.AddScoped<IMediatorHandler, MediatorHandler>();

        services.AddScoped<IRequestHandler<CustomerRegisterCommand, ValidationResult>, CustomerCommandHandler>();
        services.AddScoped<IRequestHandler<AddressAddCommand, ValidationResult>, CustomerCommandHandler>();

        services.AddScoped<INotificationHandler<CustomerRegisteredEvent>, CustomerEventHandler>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<CustomerContext>();
        
        return services;
    }
}
