using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using EFCoreSearchOptimization.Domain;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSearchOptimization;

[SimpleJob(RuntimeMoniker.Net80, baseline: true)]
[JsonExporter]
public class Performances
{
    private readonly Context _context = new(
        new DbContextOptionsBuilder<Context>()
            .UseSqlServer("Server=localhost,1433;Initial Catalog=EFCorePerformanceOptimization;Persist Security Info=False;User ID=sa;Password=1986.Yusuf+;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;Enlist=false;")
            .Options
    );
    
    [GlobalSetup]
    public void Setup()
    {
    }
    
    [Benchmark]
    public async Task<ViewAuthorBookOrderCount[]> GetBookWithAuthorFromView()
    {
        var result = await _context.ViewAuthorBookOrderCounts!
            .ToArrayAsync();
        return result;
    }
    
    [Benchmark]
    public async Task<ViewAuthorBookOrderCount[]> GetBookWithAuthorFromTable()
    {
        var result = await _context.Books!
            .Include(x=>x.Author)
            .Include(x=>x.BookOrders)
            .Select(x => new ViewAuthorBookOrderCount()
            {
                AuthorId = x.AuthorId ,
                Name = x.Author.Name,
                Surname = x.Author.Surname,
                Id = x.Id,
                Title = x.Title,
                OrderCount = x.BookOrders.Count
            })
            .Where(x=>x.OrderCount > 0)
            .ToArrayAsync();
      return result;
    }
}