using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemSales.AccessLayer.Migrations
{
    public partial class EditedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LowerCaseName",
                table: "Sale_Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "Sale_Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LowerCaseName",
                table: "Product_Category",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LowerCaseName",
                table: "Sale_Product");

            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "Sale_Product");

            migrationBuilder.DropColumn(
                name: "LowerCaseName",
                table: "Product_Category");
        }
    }
}
