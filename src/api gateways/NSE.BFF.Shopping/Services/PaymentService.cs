using Microsoft.Extensions.Options;
using NSE.BFF.Shopping.Extensions;

namespace NSE.BFF.Shopping.Services;

public class PaymentService : Service, IPaymentService
{
    private readonly HttpClient _httpClient;

    public PaymentService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.UrlPayment);
    }
}
