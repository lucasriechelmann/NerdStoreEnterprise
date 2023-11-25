using NSE.BFF.Shopping.Models;
using NSE.Core.Communication;

namespace NSE.BFF.Shopping.Services;
public interface IBasketService
{
    Task<BasketDTO> GetBasket();
    Task<ResponseResult> AddBasketItem(BasketItemDTO product);
    Task<ResponseResult> UpdateBasketItem(Guid productId, BasketItemDTO product);
    Task<ResponseResult> RemoveBasketItem(Guid productId);
    Task<ResponseResult> ApplyBasketVoucher(VoucherDTO voucher);
}
