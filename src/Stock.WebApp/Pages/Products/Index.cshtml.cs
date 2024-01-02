using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Stock.WebApp.Models;
using Stock.WebApp.Services;

namespace Stock.WebApp.Pages.Products;

public class IndexModel(ProductsService service) : PageModel
{
    [BindProperty]
    public IEnumerable<Product> ProductModels { get; set; }

    public async Task OnGet()
    {
        ProductModels = await service.GetAllAsync();
    }
}


