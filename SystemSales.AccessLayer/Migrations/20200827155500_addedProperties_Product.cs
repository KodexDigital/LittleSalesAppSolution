using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemSales.AccessLayer.Migrations
{
    public partial class addedProperties_Product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Delete",
                table: "Sale_Product",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Sale_Product",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProductImagePath",
                table: "Sale_Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Delete",
                table: "Sale_Product");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Sale_Product");

            migrationBuilder.DropColumn(
                name: "ProductImagePath",
                table: "Sale_Product");
        }
    }
}
