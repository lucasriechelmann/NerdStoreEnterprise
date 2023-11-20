using Microsoft.AspNetCore.Mvc;
using NSE.Core.Mediator;
using NSE.Customer.API.Application.Commands;
using NSE.WebAPI.Core.Controllers;

namespace NSE.Customer.API.Controllers
{
    public class CustomersController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public CustomersController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet]
        [Route("customers")]
        public async Task<IActionResult> Index()
        {
            var result = await _mediatorHandler.SendCommand(
                new CustomerRegisterCommand(Guid.NewGuid(), "Lucas Riechelmann Ramos", "lucas.riechelmann@hotmail.com", "22943295892"));
            return CustomResponse(result);
        }
    }
}
