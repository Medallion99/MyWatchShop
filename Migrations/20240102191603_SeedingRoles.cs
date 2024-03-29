﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWatchShop.Migrations
{
    /// <inheritdoc />
    public partial class SeedingRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp)
                    Values('1', 'regular', 'REGULAR', '01/01/0001 00:00:00'),
                            ('2', 'admin', 'ADMIN', '01/01/0001 00:00:00')
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
