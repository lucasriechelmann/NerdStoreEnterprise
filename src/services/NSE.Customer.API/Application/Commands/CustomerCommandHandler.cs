using FluentValidation.Results;
using MediatR;
using NSE.Core.Messages;
using NSE.Customer.API.Application.Events;
using NSE.Customer.API.Models;

namespace NSE.Customer.API.Application.Commands;
public class CustomerCommandHandler : CommandHandler, 
    IRequestHandler<CustomerRegisterCommand, ValidationResult>,
    IRequestHandler<AddressAddCommand, ValidationResult>
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<ValidationResult> Handle(CustomerRegisterCommand message, CancellationToken cancellationToken)
    {
        if(!message.IsValid()) 
            return message.ValidationResult;

        var customer = new Models.Customer(message.Id, message.Name, message.Email, message.Cpf);
        var existingCustomer = await _customerRepository.GetByCpf(customer.Cpf.Number);
        
        if (existingCustomer is not null)
        {
            AddError("This customer already exists");
            return ValidationResult;
        }

        _customerRepository.Add(customer);

        customer.AddEvent(new CustomerRegisteredEvent(customer.Id, customer.Name, customer.Cpf.Number, customer.Email.Address));

        return await PersistData(_customerRepository.UnitOfWork);
    }
    public async Task<ValidationResult> Handle(AddressAddCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid()) return message.ValidationResult;

        var address = new Address(message.Street, message.Number, message.Complement, message.District, message.ZipCode, message.City, message.State, message.CustomerId);

        _customerRepository.AddAddress(address);

        return await PersistData(_customerRepository.UnitOfWork);
    }
}
