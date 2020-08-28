using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemSales.AccessLayer.Migrations
{
    public partial class NewProperty_ProductCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Delete",
                table: "Product_Category",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Product_Category",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Delete",
                table: "Product_Category");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Product_Category");
        }
    }
}
