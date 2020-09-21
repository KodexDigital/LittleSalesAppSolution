using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemSales.AccessLayer.Migrations
{
    public partial class Refactored_Product_CategoryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Sale_Product");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Sale_Product",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Sale_Product");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Sale_Product",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
