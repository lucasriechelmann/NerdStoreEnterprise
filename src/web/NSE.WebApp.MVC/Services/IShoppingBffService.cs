using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;

public interface IShoppingBffService
{
    //Basket
    Task<BasketViewModel> GetBasket();
    Task<int> GetBasketQuantity();
    Task<ResponseResult> AddBasketItem(BasketItemViewModel item);
    Task<ResponseResult> UpdateBasketItem(Guid productId, BasketItemViewModel item);
    Task<ResponseResult> RemoveBasketItem(Guid productId);
    Task<ResponseResult> ApplyBasketVoucher(string voucher);
    //Order
    Task<ResponseResult> FinishOrder(OrderTransactionViewModel orderTransaction);
    Task<OrderViewModel> GetLastOrder();
    Task<IEnumerable<OrderViewModel>> GetListByCustomerId();
    OrderTransactionViewModel MapToOrder(BasketViewModel basket, AddressViewModel address);
}
