using NSE.Order.API.Application.DTO;
using NSE.Order.Domain.Orders;

namespace NSE.Order.API.Application.Queries;
public class OrderQueries : IOrderQueries
{
    private readonly IOrderRepository _orderRepository;

    public OrderQueries(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDTO> GetLastOrder(Guid customerId)
    {
        var order = await _orderRepository.GetLastOrder(customerId);
        return (OrderDTO)order ?? null;
    }

    public async Task<IEnumerable<OrderDTO>> GetListByCustomerId(Guid clientcustomerIdeId)
    {
        var orders = await _orderRepository.GetListByCustomerId(clientcustomerIdeId);
        return orders.Select(order => (OrderDTO)order);
    }
}
