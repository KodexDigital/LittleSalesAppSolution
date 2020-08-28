using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemApp.Logics.Abstracts;
using SystemSales.AccessLayer;

namespace SystemApp.Logics.Helpers
{
	public partial class ProductCategoryHelper<T> : LogicHelpers<T> where T:class
	{
		private readonly ApplicationDbContext context;

		private double InActiveCategory { get; set; }
		private double ActiveCategory { get; set; }

		public ProductCategoryHelper(ApplicationDbContext context)
		{
			this.context = context;
		}
		protected override double CalculateTotal()
		{
			var record =  context.ProductCategories.AsQueryable();
			return record.Count(c => c.IsActive);
		}
	}
}
