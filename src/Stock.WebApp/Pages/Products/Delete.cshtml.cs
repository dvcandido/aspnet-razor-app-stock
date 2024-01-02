using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stock.WebApp.Models;
using Stock.WebApp.Services;

namespace Stock.WebApp.Pages.Products;

public class DeleteModel(ProductsService service)  : PageModel
{
    [BindProperty]
    public Product Product { get; set; }

    public async Task OnGet(int id)
    {
        Product = await service.GetAsync(id);
    }

    public async Task<IActionResult> OnPost()
    {
        var IsSuccessStatusCode = await service.DeleteAsync(Product.Id);

        if (IsSuccessStatusCode)
        {
            TempData["success"] = "Data was deleted successfully.";
            return Redirect("Index");
        }

        TempData["failure"] = "Operation was not successful";
        return RedirectToPage("Index");

    }
    
}