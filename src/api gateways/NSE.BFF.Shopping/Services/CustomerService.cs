using Microsoft.Extensions.Options;
using NSE.BFF.Shopping.Extensions;
using NSE.BFF.Shopping.Models;
using System.Net;

namespace NSE.BFF.Shopping.Services;

public class CustomerService : Service, ICustomerService
{
    private readonly HttpClient _httpClient;

    public CustomerService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.UrlCustomer);
    }

    public async Task<AddressDTO> GetAddress()
    {
        var response = await _httpClient.GetAsync("/customer/address/");

        if (response.StatusCode == HttpStatusCode.NotFound) return null;

        HandleResponseError(response);

        return await DeserializeObjectResponse<AddressDTO>(response);
    }
}
