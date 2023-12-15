using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Stock.WebApp.Models;

public record Product
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [Required, StringLength(200)]
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [Required]
    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }   
}
