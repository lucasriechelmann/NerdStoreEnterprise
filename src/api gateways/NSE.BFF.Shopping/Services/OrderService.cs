using Microsoft.Extensions.Options;
using NSE.BFF.Shopping.Extensions;
using NSE.BFF.Shopping.Models;
using NSE.Core.Communication;
using System.Net;

namespace NSE.BFF.Shopping.Services;

public class OrderService : Service, IOrderService
{
    private readonly HttpClient _httpClient;

    public OrderService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.UrlOrder);
    }

    public async Task<ResponseResult> FinishOrder(OrderDTO order)
    {
        var orderContent = GetContent(order);

        var response = await _httpClient.PostAsync("/order/", orderContent);

        if (!HandleResponseError(response)) return await DeserializeObjectResponse<ResponseResult>(response);

        return OkReturn();
    }

    public async Task<OrderDTO> GetLastOrder()
    {
        var response = await _httpClient.GetAsync("/order/last/");

        if (response.StatusCode == HttpStatusCode.NotFound) return null;

        HandleResponseError(response);

        return await DeserializeObjectResponse<OrderDTO>(response);
    }

    public async Task<IEnumerable<OrderDTO>> GetListByClientId()
    {
        var response = await _httpClient.GetAsync("/order/list-customer/");

        if (response.StatusCode == HttpStatusCode.NotFound) return null;

        HandleResponseError(response);

        return await DeserializeObjectResponse<IEnumerable<OrderDTO>>(response);
    }

    public async Task<VoucherDTO> GetVoucherByCode(string code)
    {
        var response = await _httpClient.GetAsync($"/voucher/{code}/");

        if (response.StatusCode == HttpStatusCode.NotFound) return null;

        HandleResponseError(response);

        return await DeserializeObjectResponse<VoucherDTO>(response);
    }
}