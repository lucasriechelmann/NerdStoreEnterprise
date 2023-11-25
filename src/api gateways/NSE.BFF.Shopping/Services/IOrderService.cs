using NSE.BFF.Shopping.Models;
using NSE.Core.Communication;

namespace NSE.BFF.Shopping.Services;
public interface IOrderService
{
    Task<ResponseResult> FinishOrder(OrderDTO ordeer);
    Task<OrderDTO> GetLastOrder();
    Task<IEnumerable<OrderDTO>> GetListByClientId();

    Task<VoucherDTO> GetVoucherByCode(string code);
}
