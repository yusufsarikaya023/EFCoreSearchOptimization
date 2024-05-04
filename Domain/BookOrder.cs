using System.Text.Json.Serialization;

namespace EFCoreSearchOptimization.Domain;

public class BookOrder
{
    public int Id { get; set; }
    public int BookId { get; set; }
    [JsonIgnore]
    public Book Book { get; set; } = null!;
    
    public DateTime OrderDate { get; set; }
    public string OrderNumber { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}