using System;
using System.Collections.Generic;
using System.Text;
using SystemApp.Models.DataModels;

namespace SystemApp.Models.ViewModels
{
	public class ProductDetailsViewModel
	{
		public IEnumerable<Product> ProductList { get; set; }
		public Product SingleProduct { get; set; }
	}
}
