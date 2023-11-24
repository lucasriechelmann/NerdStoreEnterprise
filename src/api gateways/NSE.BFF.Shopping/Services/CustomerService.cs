using Microsoft.Extensions.Options;
using NSE.BFF.Shopping.Extensions;

namespace NSE.BFF.Shopping.Services;

public class CustomerService : Service, ICustomerService
{
    private readonly HttpClient _httpClient;

    public CustomerService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.UrlCustomer);
    }
}
