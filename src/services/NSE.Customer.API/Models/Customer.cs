using NSE.Core.DomainObjects;

namespace NSE.Customer.API.Models;

public class Customer : Entity, IAggregateRoot
{
    protected Customer() { }
    public Customer(Guid guid, string name, string email, string cpf)
    {
        Id = guid;
        Name = name;
        Email = new Email(email);
        Cpf = new Cpf(cpf);
        Deleted = false;
        
    }
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public Cpf Cpf { get; private set; }
    public bool Deleted { get; private set; }
    public Address Address { get; private set; }
    public void ChangeEmail(string email) => Email = new Email(email);
    public void AddAddress(Address address) => Address = address;
}
