using NSE.Core.Messages;
using NSE.Customer.API.Application.Commands.Validations;

namespace NSE.Customer.API.Application.Commands;

public class AddressAddCommand : Command
{
    public Guid CustomerId { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string Complement { get; set; }
    public string District { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string State { get; set; }

    public AddressAddCommand()
    {
    }

    public AddressAddCommand(Guid customerId, string street, string number, string complement,
        string district, string zipCode, string city, string state)
    {
        AggregateId = customerId;
        CustomerId = customerId;
        Street = street;
        Number = number;
        Complement = complement;
        District = district;
        ZipCode = zipCode;
        City = city;
        State = state;
    }

    public override bool IsValid()
    {
        ValidationResult = new AddressAddCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
