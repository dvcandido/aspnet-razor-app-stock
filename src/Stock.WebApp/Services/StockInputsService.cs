using System.Text;
using System.Text.Json;
using Stock.WebApp.Models;

namespace Stock.WebApp.Services;

public class StockInputsService(IHttpClientFactory httpClientFactory)
{
   public async Task<IEnumerable<StockInput>> GetAllAsync()
   {
      var httpClient = httpClientFactory.CreateClient("StockApi");

      using HttpResponseMessage response = await httpClient.GetAsync("stock-inputs");

      if (response.IsSuccessStatusCode)
      {
         using var contentStream = await response.Content.ReadAsStreamAsync();
         return await JsonSerializer.DeserializeAsync<IEnumerable<StockInput>>(contentStream);
      }

      return Array.Empty<StockInput>();
   }

   public async Task<bool> AddAsync(StockInput stockInput)
   {
      var jsonContent = new StringContent(JsonSerializer.Serialize(stockInput), Encoding.UTF8, "application/json");

      var httpClient = httpClientFactory.CreateClient("StockApi");

      using HttpResponseMessage response = await httpClient.PostAsync("stock-inputs", jsonContent);

      return response.IsSuccessStatusCode;
   }

}