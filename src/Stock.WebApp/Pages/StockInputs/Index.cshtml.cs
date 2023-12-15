using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stock.WebApp.Models;

namespace Stock.WebApp.Pages.StockInputs;

public class IndexModel(IHttpClientFactory httpClientFactory) : PageModel
{
    [BindProperty]
    public IEnumerable<StockInput> StockInputModels { get; set; } 

    public async Task OnGet()
    {
        var httpClient = httpClientFactory.CreateClient("StockApi");

        using HttpResponseMessage response = await httpClient.GetAsync("stock-inputs");

        if (response.IsSuccessStatusCode)
        {
            using var contentStream = await response.Content.ReadAsStreamAsync();
            StockInputModels = await JsonSerializer.DeserializeAsync<IEnumerable<StockInput>>(contentStream);
        }
    }
}