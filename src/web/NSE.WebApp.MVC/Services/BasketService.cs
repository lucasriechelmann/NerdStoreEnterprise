using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;
public class BasketService : Service, IBasketService
{
    private readonly HttpClient _httpClient;
    public BasketService(HttpClient httpClient, IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.UrlBasket);
    }
    public async Task<BasketViewModel> GetBasket()
    {
        var response = await _httpClient.GetAsync("/basket/");
        HandleResponseError(response);
        return await DeserializeObjectResponse<BasketViewModel>(response);
    }
    public async Task<ResponseResult> AddBasketItem(BasketItemViewModel product)
    {
        var itemContent = GetContent(product);
        var response = await _httpClient.PostAsync("/basket/", itemContent);
        
        if (!HandleResponseError(response)) 
            return await DeserializeObjectResponse<ResponseResult>(response);

        return OkReturn();
    }
    public async Task<ResponseResult> UpdateBasketItem(Guid productId, BasketItemViewModel product)
    {
        var itemContent = GetContent(product);
        var response = await _httpClient.PutAsync($"/basket/{productId}", itemContent);

        if (!HandleResponseError(response))
            return await DeserializeObjectResponse<ResponseResult>(response);

        return OkReturn();
    }
    public async Task<ResponseResult> RemoveBasketItem(Guid productId)
    {
        var response = await _httpClient.DeleteAsync($"/basket/{productId}");

        if (!HandleResponseError(response))
            return await DeserializeObjectResponse<ResponseResult>(response);

        return OkReturn();
    }
}
