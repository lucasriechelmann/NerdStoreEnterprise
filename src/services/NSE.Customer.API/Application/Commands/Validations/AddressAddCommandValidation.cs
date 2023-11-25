using FluentValidation;

namespace NSE.Customer.API.Application.Commands.Validations;
public class AddressAddCommandValidation : AbstractValidator<AddressAddCommand>
{
    public AddressAddCommandValidation()
    {
        RuleFor(c => c.Street)
                    .NotEmpty()
                    .WithMessage("Informe o Logradouro");

        RuleFor(c => c.Number)
            .NotEmpty()
            .WithMessage("Informe o Número");

        RuleFor(c => c.ZipCode)
            .NotEmpty()
            .WithMessage("Informe o CEP");

        RuleFor(c => c.District)
            .NotEmpty()
            .WithMessage("Informe o Bairro");

        RuleFor(c => c.City)
            .NotEmpty()
            .WithMessage("Informe o Cidade");

        RuleFor(c => c.State)
            .NotEmpty()
            .WithMessage("Informe o Estado");
    }
}
