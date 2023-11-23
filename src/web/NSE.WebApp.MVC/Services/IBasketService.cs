using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;
public interface IBasketService
{
    Task<BasketViewModel> GetBasket();
    Task<ResponseResult> AddBasketItem(BasketItemViewModel product);
    Task<ResponseResult> UpdateBasketItem(Guid productId, BasketItemViewModel product);
    Task<ResponseResult> RemoveBasketItem(Guid productId);
}
