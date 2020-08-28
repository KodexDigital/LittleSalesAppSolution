﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SystemSales.AccessLayer;

namespace SystemSales.AccessLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200825151301_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SystemApp.Models.DataModels.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<Guid?>("Product")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProductSalesDetailsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Product");

                    b.HasIndex("ProductSalesDetailsId");

                    b.ToTable("Sale_Product");
                });

            modelBuilder.Entity("SystemApp.Models.DataModels.ProductCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("OderPosition")
                        .HasColumnType("int");

                    b.Property<string>("ShortNote")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Product_Category");
                });

            modelBuilder.Entity("SystemApp.Models.DataModels.ProductSalesDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ProductSalesDetails");
                });

            modelBuilder.Entity("SystemApp.Models.DataModels.SalesOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrdeNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductSalesDetailsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProductSalesDetailsId");

                    b.ToTable("Sale_Order");
                });

            modelBuilder.Entity("SystemApp.Models.DataModels.Product", b =>
                {
                    b.HasOne("SystemApp.Models.DataModels.SalesOrder", "SalesOrder")
                        .WithMany("Products")
                        .HasForeignKey("Product");

                    b.HasOne("SystemApp.Models.DataModels.ProductSalesDetails", null)
                        .WithMany("Products")
                        .HasForeignKey("ProductSalesDetailsId");
                });

            modelBuilder.Entity("SystemApp.Models.DataModels.SalesOrder", b =>
                {
                    b.HasOne("SystemApp.Models.DataModels.ProductSalesDetails", null)
                        .WithMany("SalesOrders")
                        .HasForeignKey("ProductSalesDetailsId");
                });
#pragma warning restore 612, 618
        }
    }
}
