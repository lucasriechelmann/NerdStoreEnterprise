using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;
using System.Text;
using System.Text.Json;

namespace NSE.WebApp.MVC.Services;
public abstract class Service
{
    protected bool HandleResponseError(HttpResponseMessage response)
    {
        switch((int)response.StatusCode)
        {
            case 401:
            case 403:
            case 404:
            case 500:
                throw new CustomHttpRequestException(response.StatusCode);                
            case 400:
                return false;
        }

        response.EnsureSuccessStatusCode();
        return true;
    }

    protected async Task<T> DeserializeObjectResponse<T>(HttpResponseMessage responseMessage) =>
        JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), GetOptions());

    protected StringContent GetContent(object data)
    {
        return new StringContent(
            JsonSerializer.Serialize(data),
            Encoding.UTF8,
            "application/json");
    }

    JsonSerializerOptions GetOptions() =>
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
}
