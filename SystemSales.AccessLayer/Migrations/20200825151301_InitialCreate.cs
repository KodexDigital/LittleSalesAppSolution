using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemSales.AccessLayer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product_Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CategoryName = table.Column<string>(nullable: true),
                    ShortNote = table.Column<string>(nullable: true),
                    OderPosition = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSalesDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSalesDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sale_Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    OrdeNumber = table.Column<string>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false),
                    ProductSalesDetailsId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sale_Order_ProductSalesDetails_ProductSalesDetailsId",
                        column: x => x.ProductSalesDetailsId,
                        principalTable: "ProductSalesDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sale_Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    ProductNumber = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Product = table.Column<Guid>(nullable: true),
                    ProductSalesDetailsId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sale_Product_Sale_Order_Product",
                        column: x => x.Product,
                        principalTable: "Sale_Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sale_Product_ProductSalesDetails_ProductSalesDetailsId",
                        column: x => x.ProductSalesDetailsId,
                        principalTable: "ProductSalesDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sale_Order_ProductSalesDetailsId",
                table: "Sale_Order",
                column: "ProductSalesDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_Product_Product",
                table: "Sale_Product",
                column: "Product");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_Product_ProductSalesDetailsId",
                table: "Sale_Product",
                column: "ProductSalesDetailsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product_Category");

            migrationBuilder.DropTable(
                name: "Sale_Product");

            migrationBuilder.DropTable(
                name: "Sale_Order");

            migrationBuilder.DropTable(
                name: "ProductSalesDetails");
        }
    }
}
