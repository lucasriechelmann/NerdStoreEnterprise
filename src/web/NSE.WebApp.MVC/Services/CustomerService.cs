using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;
using System.Net;

namespace NSE.WebApp.MVC.Services;
public class CustomerService : Service, ICustomerService
{
    private readonly HttpClient _httpClient;

    public CustomerService(HttpClient httpClient, IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.UrlCustomer);
    }

    public async Task<ResponseResult> AddAddress(AddressViewModel address)
    {
        var enderecoContent = GetContent(address);

        var response = await _httpClient.PostAsync("/customer/address/", enderecoContent);

        if (!HandleResponseError(response)) 
            return await DeserializeObjectResponse<ResponseResult>(response);

        return OkReturn();
    }

    public async Task<AddressViewModel> GetAddress()
    {
        var response = await _httpClient.GetAsync("/customer/address/");

        if (response.StatusCode == HttpStatusCode.NotFound) return null;

        HandleResponseError(response);

        return await DeserializeObjectResponse<AddressViewModel>(response);
    }
}
