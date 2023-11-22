using NSE.Core.Mediator;
using NSE.Core.Messages.Integration;
using NSE.Customer.API.Application.Commands;
using NSE.MessageBus;

namespace NSE.Customer.API.Services;
public class CustomerRegisterIntegrationHandler : BackgroundService
{
    private IMessageBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public CustomerRegisterIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
    {
        _serviceProvider = serviceProvider;
        _bus = bus;
    }
    void SetRespond()
    {
        _bus.RespondAsync<UserRegisteredIntegrationEvent, ResponseMessage>(async request =>
        {
            return await CustomerRegister(request);
        });

        _bus.AdvancedBus.Connected += OnConnect;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        SetRespond();

        return Task.CompletedTask;
    }
    private void OnConnect(object s, EventArgs e) => SetRespond();
    private async Task<ResponseMessage> CustomerRegister(UserRegisteredIntegrationEvent message)
    {
        var customerCommand = new CustomerRegisterCommand(message.Id, message.Name, message.Email, message.Cpf);

        using var scope = _serviceProvider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
        var success = await mediator.SendCommand(customerCommand);

        return new ResponseMessage(success);
    }
}
