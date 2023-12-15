using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stock.WebApp.Models;

namespace Stock.WebApp.Pages.Products;

public class AddModel(IHttpClientFactory httpClientFactory) : PageModel
{
    [BindProperty]
    public Product Product { get; set; }

    public async Task<IActionResult> OnPost()
    {
        var jsonContent = new StringContent(JsonSerializer.Serialize(Product), Encoding.UTF8, "application/json");

        var httpClient = httpClientFactory.CreateClient("StockApi");

        using HttpResponseMessage response = await httpClient.PostAsync("products", jsonContent);

        if (response.IsSuccessStatusCode)
        {
            TempData["success"] = "Data was added successfully.";
            return Redirect("Index");
        }

        TempData["failure"] = "Operation was not successful";
        return RedirectToPage("Index");

    }
}
