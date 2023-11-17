using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;
public class CatalogService : Service, ICatalogService
{
    private readonly HttpClient _httpClient;

    public CatalogService(HttpClient httpClient, IOptions<AppSettings> settings)
    {
        httpClient.BaseAddress = new Uri(settings.Value.UrlCatalog);
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ProductViewModel>> GetAll()
    {
        var response = await _httpClient.GetAsync("/catalog/products/");

        HandleResponseError(response);

        return await DeserializeObjectResponse<IEnumerable<ProductViewModel>>(response);
    }

    public async Task<ProductViewModel> GetById(Guid id)
    {
        var response = await _httpClient.GetAsync($"/catalog/products/{id}");

        HandleResponseError(response);

        return await DeserializeObjectResponse<ProductViewModel>(response);
    }
}
