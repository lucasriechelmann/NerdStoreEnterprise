using NSE.Core.Messages;

namespace NSE.Customer.API.Application.Events;
public class CustomerRegisteredEvent : Event
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Cpf { get; private set; }
    public string Email { get; private set; }

    public CustomerRegisteredEvent(Guid id, string name, string cpf, string email)
    {
        AggregateId = id;
        Id = id;
        Name = name;
        Cpf = cpf;
        Email = email;
    }
}
