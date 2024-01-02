using Stock.WebApp.Models;
using System.Text;
using System.Text.Json;

namespace Stock.WebApp.Services;

public class ProductsService(IHttpClientFactory httpClientFactory)
{
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var httpClient = httpClientFactory.CreateClient("StockApi");

        using HttpResponseMessage response = await httpClient.GetAsync("products");

        if (response.IsSuccessStatusCode)
        {
            using var contentStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<Product>>(contentStream);
        }

        return Array.Empty<Product>();
    }

    public async Task<Product> GetAsync(int id)
    {
        var httpClient = httpClientFactory.CreateClient("StockApi");

        using HttpResponseMessage response = await httpClient.GetAsync($"products/{id}");

        if (response.IsSuccessStatusCode)
        {
            using var contentStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<Product>(contentStream);
        }

        return null;
    }

    public async Task<bool> AddAsync(Product product)
    {
        var jsonContent = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");

        var httpClient = httpClientFactory.CreateClient("StockApi");

        using HttpResponseMessage response = await httpClient.PostAsync("products", jsonContent);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAsync(Product product)
    {
        var jsonContent = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");

        var httpClient = httpClientFactory.CreateClient("StockApi");

        using HttpResponseMessage response = await httpClient.PutAsync($"products/{product.Id}", jsonContent);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var httpClient = httpClientFactory.CreateClient("StockApi");

        using HttpResponseMessage response = await httpClient.DeleteAsync($"products/{id}");

        return response.IsSuccessStatusCode;
    }
    
}