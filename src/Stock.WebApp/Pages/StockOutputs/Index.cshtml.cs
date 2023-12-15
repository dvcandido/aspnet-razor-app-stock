using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stock.WebApp.Models;

namespace Stock.WebApp.Pages.StockOutputs;

public class IndexModel(IHttpClientFactory httpClientFactory) : PageModel
{
    [BindProperty]
    public IEnumerable<StockOutput> StockOutputs { get; set; } = new List<StockOutput>();

    public async Task OnGet()
    {
        var httpClient = httpClientFactory.CreateClient("StockApi");

        using HttpResponseMessage response = await httpClient.GetAsync("stock-outputs");

        if (response.IsSuccessStatusCode)
        {
            using var contentStream = await response.Content.ReadAsStreamAsync();
            StockOutputs = await JsonSerializer.DeserializeAsync<IEnumerable<StockOutput>>(contentStream);
        }
    }
}