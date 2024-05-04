using System.Globalization;
using BenchmarkDotNet.Running;
using Bogus;
using EFCoreSearchOptimization;
using EFCoreSearchOptimization.Domain;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Context>(x => { x.UseSqlServer(connectionString); });
BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/addDataWithBogus", (int count, Context context) =>
    {
        for (var i = 0; i < count; i++)
        {
            Book GetBook()
            {
                return new Faker<Book>()
                        .RuleFor(x => x.Title, f => f.Lorem.Sentence())
                        .RuleFor(x => x.CreationDate, f => f.Date.Past().ToString(CultureInfo.InvariantCulture))
                        .RuleFor(x => x.ISBN, f => f.Commerce.Ean13())
                        .RuleFor(x => x.Description, f => f.Lorem.Paragraph())
                        .RuleFor(x => x.CoverLetter, f => f.Lorem.Paragraph())
                        .RuleFor(x => x.Genre, f => f.Lorem.Word())
                        .RuleFor(x => x.Language, f => f.Lorem.Word())
                        .RuleFor(x => x.Publisher, f => f.Company.CompanyName())
                        .RuleFor(x => x.PublicationDate, f => f.Date.Past().ToString(CultureInfo.InvariantCulture))
                        .RuleFor(x => x.Edition, f => f.Lorem.Word())
                        .RuleFor(x => x.Pages, f => f.Random.Number(100, 1000).ToString())
                        .RuleFor(x => x.Format, f => f.Lorem.Word())
                        .RuleFor(x => x.Weight, f => f.Random.Number(100, 1000).ToString())
                        .RuleFor(x => x.Size, f => f.Lorem.Word())
                        .RuleFor(x => x.Price, f => f.Random.Number(10, 1000).ToString())
                        .RuleFor(x => x.Stock, f => f.Random.Number(1, 100).ToString())
                        .RuleFor(x => x.Image, f => f.Image.PicsumUrl())
                        .RuleFor(x => x.ImageAlt, f => f.Lorem.Word())
                        .RuleFor(x => x.ImageTitle, f => f.Lorem.Word())
                        .RuleFor(x => x.MetaTitle, f => f.Lorem.Word())
                        .RuleFor(x => x.MetaDescription, f => f.Lorem.Word())
                        .RuleFor(x => x.MetaKeywords, f => f.Lorem.Word())
                    ;
            }
            
            var author = new Faker<Author>()
                .RuleFor(x => x.Name, f => f.Name.FirstName())
                .RuleFor(x => x.Surname, f => f.Name.LastName())
                .RuleFor(x => x.CoverLetter, f => f.Lorem.Paragraph())
                .RuleFor(x => x.CreationDate, f => f.Date.Past().ToString(CultureInfo.InvariantCulture))
                .RuleFor(x => x.BirthDate, f => f.Date.Past().ToString(CultureInfo.InvariantCulture))
                .RuleFor(x => x.Books, f => [GetBook(), GetBook(), GetBook()]);
                ;
                
            context.Authors!.Add(author);
            context.SaveChanges();
        }
    })
    .WithName("addDataWithBogus")
    .WithOpenApi();


app.MapGet("/addOrderToExistingBooks", (Context context) =>
    {
        var books = context.Books!
            .Skip(6000)
            .ToList();
        
        BookOrder GetOrder(Book book)
        {
            return new Faker<BookOrder>()
                .RuleFor(x => x.BookId, f => book.Id)
                .RuleFor(x => x.OrderDate, f => f.Date.Past())
                .RuleFor(x => x.OrderNumber, f => f.Random.Guid().ToString())
                .RuleFor(x => x.Quantity, f => f.Random.Number(1, 10))
                .RuleFor(x => x.Price, f => f.Random.Number(10, 1000))
                ;
        }
        
        foreach (var book in books)
        {
            context.BookOrders!.AddRange([GetOrder(book), GetOrder(book), GetOrder(book)]);
            context.SaveChanges();
        }
        return "Orders added";
    })
    .WithName("addOrderToExistingBooks")
    .WithOpenApi();


app.Run();