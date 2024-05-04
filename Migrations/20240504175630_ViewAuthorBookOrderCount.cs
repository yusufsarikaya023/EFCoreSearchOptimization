using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreSearchOptimization.Migrations
{
    /// <inheritdoc />
    public partial class ViewAuthorBookOrderCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                go
                CREATE VIEW dbo.ViewAuthorBookOrderCount
                WITH SCHEMABINDING
                AS
                SELECT 
                a.Id as AuthorId, 
                a.Name,
                a.Surname,
                b.Id,
                b.Title,
                COUNT_BIG(*) as OrderCount
                 from dbo.BookOrders bo
                inner join dbo.Books b on bo.BookId = b.Id
                inner join dbo.Authors a on a.Id = b.AuthorId
                GROUP by a.Id,a.Name,a.Surname,b.Title,b.Id
                go");
            
            migrationBuilder.Sql(@"
 CREATE UNIQUE CLUSTERED INDEX IDX_V2 ON dbo.ViewAuthorBookOrderCount(Id);
");
            
            migrationBuilder.Sql(@"
 CREATE NONCLUSTERED INDEX IX_ViewAuthorBookOrderCount_Search on dbo.ViewAuthorBookOrderCount (OrderCount);
");
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW dbo.ViewAuthorBookOrderCount");
        }
    }
}
