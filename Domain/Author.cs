using System.Text.Json.Serialization;

namespace EFCoreSearchOptimization.Domain;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string CoverLetter { get; set; } = null!;
    public string CreationDate { get; set; } = null!;
    public string? BirthDate { get; set; }
    
    [JsonIgnore]
    public List<Book> Books { get; set; } = [];
    
}