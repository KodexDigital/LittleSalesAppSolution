using System;
using System.Collections.Generic;
using System.Repository.Admin;
using System.Text;
using System.Threading.Tasks;
using SystemApp.Models.DataModels;

namespace System.Repository.Products
{
	public interface IProductsRepository : IDefaultRepository<Product>
	{
		Task Update(Product product);
	}
}
