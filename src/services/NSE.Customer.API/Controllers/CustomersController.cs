using Microsoft.AspNetCore.Mvc;
using NSE.Core.Mediator;
using NSE.Customer.API.Application.Commands;
using NSE.Customer.API.Models;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.User;

namespace NSE.Customer.API.Controllers
{
    public class CustomersController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAspNetUser _user;

        public CustomersController(IMediatorHandler mediatorHandler, ICustomerRepository customerRepository,
            IAspNetUser user)
        {
            _mediatorHandler = mediatorHandler;
            _customerRepository = customerRepository;
            _user = user;
        }

        [HttpGet("customer/address")]
        public async Task<IActionResult> GetAddress()
        {
            var address = await _customerRepository.GetAddressById(_user.GetUserId());

            return address is null ? NotFound() : CustomResponse(address);
        }

        [HttpPost("customer/address")]
        public async Task<IActionResult> AddAddress(AddressAddCommand address)
        {
            address.CustomerId = _user.GetUserId();
            return CustomResponse(await _mediatorHandler.SendCommand(address));
        }
    }
}
