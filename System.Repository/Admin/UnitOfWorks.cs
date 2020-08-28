using System;
using System.Collections.Generic;
using System.Repository.Category;
using System.Repository.Products;
using System.Text;
using System.Threading.Tasks;
using SystemSales.AccessLayer;

namespace System.Repository.Admin
{
	public class UnitOfWorks : IUnitOfWorks
	{
		private readonly ApplicationDbContext dbContext;

		public UnitOfWorks(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
			Category = new CategoryRepository(dbContext);
			Products = new ProductsRepository(dbContext);
		}
		public ICategoryRepository Category { get; private set; }

		public IProductsRepository Products { get; private set; }

		public async Task Save()
		{
			await dbContext.SaveChangesAsync();
		}

		public void Dispose()
		{
			dbContext.Dispose();
		}

	}
}
