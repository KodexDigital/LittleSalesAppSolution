using System;
using System.Collections.Generic;
using System.Linq;
using SystemApp.Models.DataModels;
using SystemSales.AccessLayer;

namespace BusinessLogics
{
	public class CartLogic
	{
		private readonly ApplicationDbContext context;
		internal List<Product> ItemOnCartList;
		public CartLogic(ApplicationDbContext context)
		{
			this.context = context;
		}

		internal List<Product> AllProducts() => context.Products.ToList();

		public Product Find(Guid id)
		{
			ItemOnCartList = AllProducts();
			var productItem = ItemOnCartList.Where(p => p.Id.Equals(id)).FirstOrDefault();
			return productItem;
		}

	}
}
