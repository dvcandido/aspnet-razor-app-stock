using System.Text;
using System.Text.Json;
using Stock.WebApp.Models;

namespace Stock.WebApp.Services;

public class StockOutputsService(IHttpClientFactory httpClientFactory)
{
    public async Task<IEnumerable<StockOutput>> GetAllAsync()
    {
        var httpClient = httpClientFactory.CreateClient("StockApi");

        using HttpResponseMessage response = await httpClient.GetAsync("stock-outputs");

        if (response.IsSuccessStatusCode)
        {
            using var contentStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<StockOutput>>(contentStream);
        }

        return Array.Empty<StockOutput>();
    }

    public async Task<bool> AddAsync(StockOutput stockOutput)
    {
        var jsonContent = new StringContent(JsonSerializer.Serialize(stockOutput), Encoding.UTF8, "application/json");

        var httpClient = httpClientFactory.CreateClient("StockApi");

        using HttpResponseMessage response = await httpClient.PostAsync("stock-outputs", jsonContent);

        return response.IsSuccessStatusCode;
    }
}