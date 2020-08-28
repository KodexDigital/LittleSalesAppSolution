using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SystemApp.Models.DataModels;

namespace SystemSales.AccessLayer
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext() { }
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Product>().ToTable("Sale_Product");
			modelBuilder.Entity<SalesOrder>().ToTable("Sale_Order");
			modelBuilder.Entity<ProductCategory>().ToTable("Product_Category");
		}


		public DbSet<Product> Products { get; set; }
		public DbSet<SalesOrder> SalesOrders { get; set; }
		public DbSet<ProductCategory> ProductCategories { get; set; }
		public DbSet<ProductSalesDetails> ProductSalesDetails { get; set; }

	}
}
