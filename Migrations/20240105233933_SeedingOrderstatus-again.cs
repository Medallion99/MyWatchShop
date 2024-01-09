using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWatchShop.Migrations
{
    /// <inheritdoc />
    public partial class SeedingOrderstatusagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO OrderStatuses (Id, StatusName, DateAdded, DateUpdated, DateDeleted)
                                                            Values('1', 'Pending', '01/01/0001 00:00:00', '01/01/0001 00:00:00', '01/01/0001 00:00:00'),
                                                            ('2', 'Shipped', '01/01/0001 00:00:00', '01/01/0001 00:00:00', '01/01/0001 00:00:00'),
                                                            ('3', 'Delivered', '01/01/0001 00:00:00', '01/01/0001 00:00:00', '01/01/0001 00:00:00')
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
