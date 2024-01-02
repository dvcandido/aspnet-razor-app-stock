using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stock.WebApp.Models;
using Stock.WebApp.Services;

namespace Stock.WebApp.Pages.Products;

public class EditModel(ProductsService service) : PageModel
{
    [BindProperty]
    public Product Product { get; set; }

    public async Task OnGet(int id)
    {
       Product = await service.GetAsync(id);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        var IsSuccessStatusCode = await service.UpdateAsync(Product);

        if (IsSuccessStatusCode)
        {
            TempData["success"] = "Data was updated successfully.";
            return Redirect("Index");
        }

        TempData["failure"] = "Operation was not successful";
        return RedirectToPage("Index");

    }
    
}