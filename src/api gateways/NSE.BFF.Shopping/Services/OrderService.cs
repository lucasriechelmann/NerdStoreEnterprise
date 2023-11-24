using Microsoft.Extensions.Options;
using NSE.BFF.Shopping.Extensions;

namespace NSE.BFF.Shopping.Services;

public class OrderService : Service, IOrderService
{
    private readonly HttpClient _httpClient;

    public OrderService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.UrlOrder);
    }
}