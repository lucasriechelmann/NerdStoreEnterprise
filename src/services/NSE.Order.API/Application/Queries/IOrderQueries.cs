using NSE.Order.API.Application.DTO;

namespace NSE.Order.API.Application.Queries;

public interface IOrderQueries
{
    Task<OrderDTO> GetLastOrder(Guid customerId);
    Task<IEnumerable<OrderDTO>> GetListByCustomerId(Guid clientcustomerIdeId);
}
