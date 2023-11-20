using MediatR;

namespace NSE.Customer.API.Application.Events;
public class CustomerEventHandler : INotificationHandler<CustomerRegisteredEvent>
{
    public Task Handle(CustomerRegisteredEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
