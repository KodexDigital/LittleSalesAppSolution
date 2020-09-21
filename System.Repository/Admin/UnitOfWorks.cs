using System;
using System.Collections.Generic;
using System.Repository.Category;
using System.Repository.Orders;
using System.Repository.Products;
using System.Repository.Store;
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
			OrderHeader = new OrderHeaderRepository(dbContext);
			OrderDetails = new OrderDetailsRepository(dbContext);
			Store = new StoreRepository(dbContext);
		}
		public ICategoryRepository Category { get; private set; }

		public IProductsRepository Products { get; private set; }

		public IOrderDetailsRepository OrderDetails { get; private set; }

		public IOrderHeaderRepository OrderHeader { get; private set; }

		public IStoreRepository Store { get; private set; }

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
