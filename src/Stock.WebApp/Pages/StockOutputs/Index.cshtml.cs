using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stock.WebApp.Models;
using Stock.WebApp.Services;

namespace Stock.WebApp.Pages.StockOutputs;

public class IndexModel(StockOutputsService service) : PageModel
{
    [BindProperty]
    public IEnumerable<StockOutput> StockOutputs { get; set; } = new List<StockOutput>();

    public async Task OnGet()
    {
        StockOutputs = await service.GetAllAsync();
    }
}