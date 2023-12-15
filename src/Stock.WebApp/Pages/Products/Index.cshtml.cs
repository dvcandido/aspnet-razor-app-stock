using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Stock.WebApp.Models;

namespace Stock.WebApp.Pages.Products;

public class IndexModel(IHttpClientFactory httpClientFactory) : PageModel
{
    [BindProperty]
    public IEnumerable<Product> ProductModels { get; set; }

    public async Task OnGet()
    {
        var httpClient = httpClientFactory.CreateClient("StockApi");

        using HttpResponseMessage response = await httpClient.GetAsync("products");

        if (response.IsSuccessStatusCode)
        {
            using var contentStream = await response.Content.ReadAsStreamAsync();
            ProductModels = await JsonSerializer.DeserializeAsync<IEnumerable<Product>>(contentStream);
        }
    }
}


