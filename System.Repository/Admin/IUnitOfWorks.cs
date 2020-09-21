using System;
using System.Collections.Generic;
using System.Repository.Category;
using System.Repository.Orders;
using System.Repository.Products;
using System.Repository.Store;
using System.Text;
using System.Threading.Tasks;

namespace System.Repository.Admin
{
	public interface IUnitOfWorks : IDisposable
	{
		ICategoryRepository Category { get; }
		IProductsRepository Products { get; }
		IOrderDetailsRepository OrderDetails { get; }
		IOrderHeaderRepository OrderHeader { get; }
		IStoreRepository Store { get; }
		Task Save();
	}
}
