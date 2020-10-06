using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using SystemApp.Models.Carts;
using SystemApp.Models.DataModels;
using SystemSales.AccessLayer;

namespace SystemApp.Logics.BusinessLogics
{
	public class CartLogic
	{
		private readonly ApplicationDbContext context;
		public CartLogic(ApplicationDbContext context)
		{
			this.context = context;
		}

		//private List<Product> GetAllProducts() => context.Products.ToList();

		//public Product FindAndGetProduct(Guid id)
		//{
		//	//List<Product> ItemOnCartList = GetAllProducts();
		//	return context.Products.FirstOrDefault(x => x.Id.Equals(id));
		//}

		public IEnumerable<Product> FindAndGetProduct(Guid id)
		{
			yield return context.Products.FirstOrDefault(x => x.Id.Equals(id));
		}
	}
}
