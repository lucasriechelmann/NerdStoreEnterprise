using FluentValidation;

namespace NSE.Customer.API.Application.Commands.Validations;
public class CustomerRegisterCommandValidation : AbstractValidator<CustomerRegisterCommand>
{
    public CustomerRegisterCommandValidation()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid customer id");

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Invalid customer name");

        RuleFor(c => c.Cpf)
            .Must(ValidateCpf)
            .WithMessage("Invalid customer cpf");

        RuleFor(c => c.Email)
            .Must(ValidateEmail)
            .WithMessage("Invalid customer email");
    }

    protected static bool ValidateCpf(string cpf) => Core.DomainObjects.Cpf.Validate(cpf);
    protected static bool ValidateEmail(string email) => Core.DomainObjects.Email.Validate(email);
}
