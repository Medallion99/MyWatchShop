using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWatchShop.Migrations
{
    /// <inheritdoc />
    public partial class ToSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO Products (Id, ProductName, OldPrice, NewPrice, Stars, ProductDescription, ImageUrl, DateAdded, DateUpdated, DateDeleted)
                VALUES
                ('1', 'Rolex', 60000.00, 50000.00, 5, 'Such a great piece of fashion masterclass', '',
                '1/1/0001 00:00:00 AM', '1/1/0001 00:00:00 AM', '1/1/0001 00:00:00 AM'),
                ('2', 'Omega', 32000.00, 25000.00, 5, 'Such a smart piece, you would love this', '',
                '1/1/0001 00:00:00 AM', '1/1/0001 00:00:00 AM', '1/1/0001 00:00:00 AM'),
                ('3', 'Patek-Philip', 50000.00, 40000.00, 5, 'You would surely love the sleekness of this one', '',
                '1/1/0001 00:00:00 AM', '1/1/0001 00:00:00 AM', '1/1/0001 00:00:00 AM'),
                ('4', 'Cartier', 25000.00, 20000.00, 4, 'This is a really great fashionable fit for all your occasions', '',
                '1/1/0001 00:00:00 AM', '1/1/0001 00:00:00 AM', '1/1/0001 00:00:00 AM')
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
