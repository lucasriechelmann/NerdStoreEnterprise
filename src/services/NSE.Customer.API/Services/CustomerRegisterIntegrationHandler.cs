
using EasyNetQ;
using FluentValidation.Results;
using NSE.Core.Mediator;
using NSE.Core.Messages.Integration;
using NSE.Customer.API.Application.Commands;

namespace NSE.Customer.API.Services;
public class CustomerRegisterIntegrationHandler : BackgroundService
{
    private IBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public CustomerRegisterIntegrationHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _bus = RabbitHutch.CreateBus("host=localhost:5672");
        _bus.Rpc.Respond<UserRegisteredIntegrationEvent, ResponseMessage>(async request =>
        {
            return new ResponseMessage(await CustomerRegister(request));
        });

        return Task.CompletedTask;
    }

    private async Task<ValidationResult> CustomerRegister(UserRegisteredIntegrationEvent message)
    {
        var customerCommand = new CustomerRegisterCommand(message.Id, message.Name, message.Email, message.Cpf);

        using var scope = _serviceProvider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
        var success = await mediator.SendCommand(customerCommand);

        return success;
    }
}
