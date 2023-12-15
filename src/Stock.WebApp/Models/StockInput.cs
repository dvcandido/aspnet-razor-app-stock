using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Stock.WebApp.Models;

public record StockInput
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [Required]
    [JsonPropertyName("productId")]
    public int ProductId { get; set; }
    
    [Required]
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
    
    [Required]
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("product")]
    public Product? Product { get; set; } 
}