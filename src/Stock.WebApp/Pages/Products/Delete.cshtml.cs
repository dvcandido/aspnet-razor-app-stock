using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stock.WebApp.Models;

namespace Stock.WebApp.Pages.Products;

public class DeleteModel(IHttpClientFactory httpClientFactory) : PageModel
{
    [BindProperty]
    public Product Product { get; set; }

    public async Task OnGet(int id)
    {
        var httpClient = httpClientFactory.CreateClient("StockApi");

        using HttpResponseMessage response = await httpClient.GetAsync($"products/{id}");

        if (response.IsSuccessStatusCode)
        {
            using var contentStream = await response.Content.ReadAsStreamAsync();
            Product = await JsonSerializer.DeserializeAsync<Product>(contentStream);
        }
    }

    public async Task<IActionResult> OnPost()
    {
        var httpClient = httpClientFactory.CreateClient("StockApi");

        using HttpResponseMessage response = await httpClient.DeleteAsync($"products/{Product.Id}");

        if (response.IsSuccessStatusCode)
        {
            TempData["success"] = "Data was deleted successfully.";
            return Redirect("Index");
        }

        TempData["failure"] = "Operation was not successful";
        return RedirectToPage("Index");

    }
    
}