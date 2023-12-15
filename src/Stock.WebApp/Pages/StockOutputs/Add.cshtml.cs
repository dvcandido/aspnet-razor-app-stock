using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stock.WebApp.Models;

namespace Stock.WebApp.Pages.StockOutputs;

public class AddModel(IHttpClientFactory httpClientFactory) : PageModel
{
    [BindProperty]
    public StockOutput StockOutput { get; set; }

    public async Task<IActionResult> OnPost()
    {
        var jsonContent = new StringContent(JsonSerializer.Serialize(StockOutput), Encoding.UTF8, "application/json");

        var httpClient = httpClientFactory.CreateClient("StockApi");

        using HttpResponseMessage response = await httpClient.PostAsync("stock-outputs", jsonContent);

        if (response.IsSuccessStatusCode)
        {
            TempData["success"] = "Data was added successfully.";
            return Redirect("Index");
        }

        TempData["failure"] = "Operation was not successful";
        return RedirectToPage("Index");

    }
}