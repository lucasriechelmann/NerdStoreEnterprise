using Microsoft.Extensions.Options;
using NSE.BFF.Shopping.Extensions;
using NSE.BFF.Shopping.Models;
using NSE.Core.Communication;

namespace NSE.BFF.Shopping.Services;

public class BasketService : Service, IBasketService
{
    private readonly HttpClient _httpClient;

    public BasketService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.UrlBasket);
    }
    public async Task<BasketDTO> GetBasket()
    {
        var response = await _httpClient.GetAsync("/basket/");
        HandleResponseError(response);

        return await DeserializeObjectResponse<BasketDTO>(response);
    }
    public async Task<ResponseResult> AddBasketItem(BasketItemDTO product)
    {
        var itemContent = GetContent(product);

        var response = await _httpClient.PostAsync("/basket/", itemContent);
        
        if (!HandleResponseError(response)) 
            return await DeserializeObjectResponse<ResponseResult>(response);

        return OkReturn();
    }
    public async Task<ResponseResult> UpdateBasketItem(Guid productId, BasketItemDTO product)
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
    public async Task<ResponseResult> ApplyBasketVoucher(VoucherDTO voucher)
    {
        var itemContent = GetContent(voucher);

        var response = await _httpClient.PostAsync("/basket/apply-voucher", itemContent);

        if (!HandleResponseError(response))
            return await DeserializeObjectResponse<ResponseResult>(response);
        
        return OkReturn();
    }
}