namespace EFCoreSearchOptimization.Domain;

public class ViewAuthorBookOrderCount
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public int AuthorId { get; set; }
    public string Title { get; set; } = null!;
    public long OrderCount { get; set; }
}