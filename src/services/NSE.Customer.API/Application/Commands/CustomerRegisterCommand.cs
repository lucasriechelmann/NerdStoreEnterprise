using NSE.Core.Messages;
using NSE.Customer.API.Application.Commands.Validations;

namespace NSE.Customer.API.Application.Commands;
public class CustomerRegisterCommand: Command
{
    public CustomerRegisterCommand(Guid id, string name, string email, string cpf)
    {
        Id = id;
        Name = name;
        Email = email;
        Cpf = cpf;
    }

    public override bool IsValid()
    {
        ValidationResult = new CustomerRegisterCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Cpf { get; private set; }
}
