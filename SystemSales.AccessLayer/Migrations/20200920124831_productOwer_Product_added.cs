using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemSales.AccessLayer.Migrations
{
    public partial class productOwer_Product_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductOwnerId",
                table: "Sale_Product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sale_Product_ProductOwnerId",
                table: "Sale_Product",
                column: "ProductOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_Product_AspNetUsers_ProductOwnerId",
                table: "Sale_Product",
                column: "ProductOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sale_Product_AspNetUsers_ProductOwnerId",
                table: "Sale_Product");

            migrationBuilder.DropIndex(
                name: "IX_Sale_Product_ProductOwnerId",
                table: "Sale_Product");

            migrationBuilder.DropColumn(
                name: "ProductOwnerId",
                table: "Sale_Product");
        }
    }
}
