using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Core.Mediator;
using NSE.Order.API.Application.Commands;
using NSE.Order.API.Application.Queries;
using NSE.Order.Domain.Orders;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.User;

namespace NSE.Order.API.Controllers;
[Authorize]
public class OrderController : MainController
{
    private readonly IMediatorHandler _mediator;
    private readonly IAspNetUser _user;
    private readonly IOrderQueries _orderQueries;

    public OrderController(IMediatorHandler mediator, IAspNetUser user, IOrderQueries orderQueries)
    {
        _mediator = mediator;
        _user = user;
        _orderQueries = orderQueries;
    }

    [HttpPost("order")]
    public async Task<IActionResult> AddOrder(AddOrderCommand order)
    {
        order.CustomerId = _user.GetUserId();
        return CustomResponse(await _mediator.SendCommand(order));
    }

    [HttpGet("pedido/ultimo")]
    public async Task<IActionResult> UltimoPedido()
    {
        var order = await _orderQueries.GetLastOrder(_user.GetUserId());

        if(order is null) 
            return NotFound();

        return CustomResponse(order);
    }

    [HttpGet("pedido/lista-cliente")]
    public async Task<IActionResult> ListaPorCliente()
    {
        var orders = await _orderQueries.GetListByCustomerId(_user.GetUserId());

        if (orders is null)
            return NotFound();

        return CustomResponse(orders);
    }
}
