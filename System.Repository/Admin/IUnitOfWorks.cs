using System;
using System.Collections.Generic;
using System.Repository.Category;
using System.Repository.Products;
using System.Text;
using System.Threading.Tasks;

namespace System.Repository.Admin
{
	public interface IUnitOfWorks : IDisposable
	{
		ICategoryRepository Category { get; }
		IProductsRepository Products { get; }
		Task Save();
	}
}
