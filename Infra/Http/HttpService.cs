
using Db1HealthPanelBack.Infra.Shared.Extensions;

namespace Db1HealthPanelBack.Infra.Http;

public class HttpService
{
    private readonly HttpClient _httpClient;

    public HttpService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<T?> Get<T>(RequestData request) where T : class
    {
        AddHeaders(request.Headers!);

        var response = await _httpClient.GetAsync(new Uri(request.Uri!));

        return await GetContent<T>(response);
    }

    private async Task<T?> GetContent<T>(HttpResponseMessage response) where T : class
    {
        if (!response.IsSuccessStatusCode)
            return default;
        
        var responseContent = await response.Content.ReadAsStreamAsync();
        return await responseContent.Deserialize<T>();
    }

    private void AddHeaders(Dictionary<string, string> headers)
    {
        foreach (var header in headers)
        {
            if (_httpClient.DefaultRequestHeaders.Any())
            {
                var values = _httpClient.DefaultRequestHeaders.GetValues(header.Key);

                if (values.Any()) continue;
            }

            _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        }
    }
}