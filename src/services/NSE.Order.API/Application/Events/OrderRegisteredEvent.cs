using NSE.Core.Messages;

namespace NSE.Order.API.Application.Events;
public class OrderRegisteredEvent : Event
{
    public OrderRegisteredEvent(Guid orderId, Guid customerId)
    {
        OrderId = orderId;
        CustomerId = customerId;
    }

    public Guid OrderId { get; private set; }
    public Guid CustomerId { get; private set; }
}
