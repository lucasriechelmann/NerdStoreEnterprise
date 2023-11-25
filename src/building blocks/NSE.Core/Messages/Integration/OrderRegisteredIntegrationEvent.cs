namespace NSE.Core.Messages.Integration;

public class OrderRegisteredIntegrationEvent : IntegrationEvent
{
    public OrderRegisteredIntegrationEvent(Guid customerId)
    {
        CustomerId = customerId;
    }

    public Guid CustomerId { get; private set; }
}
