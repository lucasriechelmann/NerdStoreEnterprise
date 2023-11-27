using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;
public class ShoppingBffService : Service, IShoppingBffService
{
    private readonly HttpClient _httpClient;

    public ShoppingBffService(HttpClient httpClient, IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.UrlShoppingBff);
    }
    #region Basket
    public async Task<BasketViewModel> GetBasket()
    {
        var response = await _httpClient.GetAsync("/shopping/basket/");

        HandleResponseError(response);

        return await DeserializeObjectResponse<BasketViewModel>(response);
    }
    public async Task<int> GetBasketQuantity()
    {
        var response = await _httpClient.GetAsync("/shopping/basket-quantity/");

        HandleResponseError(response);

        return await DeserializeObjectResponse<int>(response);
    }
    public async Task<ResponseResult> AddBasketItem(BasketItemViewModel item)
    {
        var itemContent = GetContent(item);

        var response = await _httpClient.PostAsync("/shopping/basket/items/", itemContent);

        if (!HandleResponseError(response)) return await DeserializeObjectResponse<ResponseResult>(response);

        return OkReturn();
    }
    public async Task<ResponseResult> UpdateBasketItem(Guid productId, BasketItemViewModel item)
    {
        var itemContent = GetContent(item);

        var response = await _httpClient.PutAsync($"/shopping/basket/items/{productId}", itemContent);

        if (!HandleResponseError(response)) return await DeserializeObjectResponse<ResponseResult>(response);

        return OkReturn();
    }
    public async Task<ResponseResult> RemoveBasketItem(Guid productId)
    {
        var response = await _httpClient.DeleteAsync($"/shopping/basket/items/{productId}");

        if (!HandleResponseError(response)) return await DeserializeObjectResponse<ResponseResult>(response);

        return OkReturn();
    }
    public async Task<ResponseResult> ApplyBasketVoucher(string voucher)
    {
        var itemContent = GetContent(voucher);

        var response = await _httpClient.PostAsync("/shopping/basket/apply-voucher/", itemContent);

        if (!HandleResponseError(response)) return await DeserializeObjectResponse<ResponseResult>(response);

        return OkReturn();
    }
    #endregion
    #region Order
    public async Task<ResponseResult> FinishOrder(OrderTransactionViewModel orderTransaction)
    {
        var orderContent = GetContent(orderTransaction);

        var response = await _httpClient.PostAsync("/shopping/order/", orderContent);

        if (!HandleResponseError(response)) return await DeserializeObjectResponse<ResponseResult>(response);

        return OkReturn();
    }
    public async Task<OrderViewModel> GetLastOrder()
    {
        var response = await _httpClient.GetAsync("/shopping/order/last/");

        HandleResponseError(response);

        return await DeserializeObjectResponse<OrderViewModel>(response);
    }
    public async Task<IEnumerable<OrderViewModel>> GetListByCustomerId()
    {
        var response = await _httpClient.GetAsync("/shopping/order/list-customer/");

        HandleResponseError(response);

        return await DeserializeObjectResponse<IEnumerable<OrderViewModel>>(response);
    }
    public OrderTransactionViewModel MapToOrder(BasketViewModel basket, AddressViewModel address)
    {
        var order = new OrderTransactionViewModel
        {
            TotalValue = basket.TotalValue,
            Items = basket.Items,
            Discount = basket.Discount,
            VoucherUsed = basket.VoucherUsed,
            VoucherCode = basket.Voucher?.Code
        };

        if (address is not null)
        {
            order.Address = new AddressViewModel
            {
                Street = address.Street,
                Number = address.Number,
                District = address.District,
                ZipCode = address.ZipCode,
                Complement = address.Complement,
                City = address.City,
                State = address.State
            };
        }

        return order;
    }
    #endregion
}
