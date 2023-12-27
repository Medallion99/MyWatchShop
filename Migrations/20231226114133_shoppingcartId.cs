using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWatchShop.Migrations
{
    /// <inheritdoc />
    public partial class shoppingcartId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ShoppingCarts_CartId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ShoppingCarts");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "Products",
                newName: "ShoppingCartId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CartId",
                table: "Products",
                newName: "IX_Products_ShoppingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ShoppingCarts_ShoppingCartId",
                table: "Products",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ShoppingCarts_ShoppingCartId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ShoppingCartId",
                table: "Products",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ShoppingCartId",
                table: "Products",
                newName: "IX_Products_CartId");

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "ShoppingCarts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ShoppingCarts_CartId",
                table: "Products",
                column: "CartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id");
        }
    }
}
