using System.Text.Json;
using System.Text;
using System.Net;

namespace NSE.BFF.Shopping.Services;

public class Service
{
    protected bool HandleResponseError(HttpResponseMessage response)
    {
        if (response.StatusCode == HttpStatusCode.BadRequest) return false;

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
