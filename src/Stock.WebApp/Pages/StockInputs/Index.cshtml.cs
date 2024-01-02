using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stock.WebApp.Models;
using Stock.WebApp.Services;

namespace Stock.WebApp.Pages.StockInputs;

public class IndexModel(StockInputsService service) : PageModel
{
    [BindProperty]
    public IEnumerable<StockInput> StockInputModels { get; set; } 

    public async Task OnGet()
    {
        StockInputModels = await service.GetAllAsync();
    }
}