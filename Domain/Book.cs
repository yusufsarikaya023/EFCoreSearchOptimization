namespace EFCoreSearchOptimization.Domain;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    
    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;
    
    public string CreationDate { get; set; } = null!;
    public string? ISBN { get; set; }
    public string? Description { get; set; }
    public string? CoverLetter { get; set; }
    public string? Genre { get; set; }
    public string? Language { get; set; }
    public string? Publisher { get; set; }
    public string? PublicationDate { get; set; }
    public string? Edition { get; set; }
    public string? Pages { get; set; }
    public string? Format { get; set; }
    public string? Weight { get; set; }
    public string? Size { get; set; }
    public string? Price { get; set; }
    public string? Stock { get; set; }
    
    public string? Image { get; set; }
    public string? ImageAlt { get; set; }
    public string? ImageTitle { get; set; }
    
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? MetaKeywords { get; set; }
    
    public List<BookOrder> BookOrders { get; set; } = new();
}