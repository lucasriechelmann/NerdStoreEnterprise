using Microsoft.Extensions.Options;
using NSE.BFF.Shopping.Extensions;
using NSE.BFF.Shopping.Models;

namespace NSE.BFF.Shopping.Services;

public class CatalogService : Service, ICatalogService
{
    private readonly HttpClient _httpClient;
    public CatalogService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.UrlCatalog);
    }

    public async Task<ProductDTO> GetById(Guid id)
    {
        var response = await _httpClient.GetAsync($"/catalog/products/{id}");

        HandleResponseError(response);

        return await DeserializeObjectResponse<ProductDTO>(response);
    }

    public async Task<IEnumerable<ProductDTO>> GetItems(IEnumerable<Guid> ids)
    {
        var idsRequest = string.Join(",", ids);

        var response = await _httpClient.GetAsync($"/catalog/products/list/{idsRequest}");

        HandleResponseError(response);

        return await DeserializeObjectResponse<IEnumerable<ProductDTO>>(response);
    }
}
