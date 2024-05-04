using EFCoreSearchOptimization.Domain;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSearchOptimization;

public class Context : DbContext
{
    public Context()
    {
    }

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    public DbSet<Book>? Books { get; set; }
    public DbSet<Author>? Authors { get; set; }
    public DbSet<BookOrder>? BookOrders { get; set; }
    public DbSet<ViewAuthorBookOrderCount>? ViewAuthorBookOrderCounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ViewAuthorBookOrderCount>()
            .HasNoKey()
            .ToView("ViewAuthorBookOrderCount");
    }
}