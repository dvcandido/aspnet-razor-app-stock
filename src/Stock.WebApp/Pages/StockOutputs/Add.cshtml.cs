using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stock.WebApp.Models;
using Stock.WebApp.Services;

namespace Stock.WebApp.Pages.StockOutputs;

public class AddModel(StockOutputsService stockOutputsService, ProductsService productsService) : PageModel
{
    [BindProperty]
    public StockOutput StockOutput { get; set; }

    public SelectList Products { get; set; }

    public async Task OnGet()
    {
        Products = new SelectList(await productsService.GetAllAsync(), nameof(Product.Id), nameof(Product.Name));
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            Products = new SelectList(await productsService.GetAllAsync(), nameof(Product.Id), nameof(Product.Name));

            return Page();
        }

       var IsSuccessStatusCode = await stockOutputsService.AddAsync(StockOutput);

        if (IsSuccessStatusCode)
        {
            TempData["success"] = "Data was added successfully.";
            return Redirect("Index");
        }

        TempData["failure"] = "Operation was not successful";
        return RedirectToPage("Index");

    }
}