using System;
using System.Collections.Generic;
using System.Text;
using SystemApp.Models.DataModels;
using SystemSales.AccessLayer;
using System.Repository.Admin;
using System.Threading.Tasks;
using System.Linq;

namespace System.Repository.Products
{
	public class ProductsRepository : DefaultRepository<Product>, IProductsRepository
	{
		private readonly ApplicationDbContext db;

		public ProductsRepository(ApplicationDbContext db) : base(db)
		{
			this.db = db;
		}

		public async Task Update(Product product)
		{
			var objFromDb = db.Products.FirstOrDefault(s => s.Id.Equals(product.Id));

			objFromDb.ProductName = product.ProductName;
			objFromDb.LowerCaseName = product.ProductName.TrimStart().Replace(" ", "-").ToLower().TrimEnd();
			objFromDb.CategoryId = product.CategoryId;
			objFromDb.Description = product.Description;
			objFromDb.Price = product.Price;
			objFromDb.IsActive = product.IsActive;
			objFromDb.ProductImagePath = product.ProductImagePath;
			objFromDb.CreatedAt = product.CreatedAt;
			await db.SaveChangesAsync();
		}
	}
}
