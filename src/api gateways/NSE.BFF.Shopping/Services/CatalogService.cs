using Microsoft.Extensions.Options;
using NSE.BFF.Shopping.Extensions;

namespace NSE.BFF.Shopping.Services;

public class CatalogService : Service, ICatalogService
{
    private readonly HttpClient _httpClient;
    public CatalogService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.UrlCatalog);
    }
}
