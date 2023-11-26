using MediatR;
using NSE.Core.Messages.Integration;
using NSE.MessageBus;

namespace NSE.Order.API.Application.Events;
public class OrderEventHandler : INotificationHandler<OrderRegisteredEvent>
{
    private readonly IMessageBus _bus;
    public OrderEventHandler(IMessageBus bus)
    {
        _bus = bus;
    }
    public async Task Handle(OrderRegisteredEvent message, CancellationToken cancellationToken) =>
        await _bus.PublishAsync(new OrderRegisteredIntegrationEvent(message.CustomerId));
}
