using Microsoft.EntityFrameworkCore;
using NSE.Basket.API.Data;
using NSE.Core.Messages.Integration;
using NSE.MessageBus;

namespace NSE.Basket.API.Services;
public class BasketIntegrationHandler : BackgroundService
{
    private readonly IMessageBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public BasketIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
    {
        _bus = bus;
        _serviceProvider = serviceProvider;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        SetSubscribers();
        return Task.CompletedTask;
    }

    private void SetSubscribers()
    {
        _bus.SubscribeAsync<OrderRegisteredIntegrationEvent>("OrderRegistered", async request =>
        await ClearBasketCustomer(request));
    }
    private async Task ClearBasketCustomer(OrderRegisteredIntegrationEvent message)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<BasketContext>();

        var customer = await context.BasketCustomers
            .FirstOrDefaultAsync(c => c.CustomerId == message.CustomerId);

        if (customer != null)
        {
            context.BasketCustomers.Remove(customer);
            await context.SaveChangesAsync();
        }
    }
}
