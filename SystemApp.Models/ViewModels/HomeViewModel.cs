using System;
using System.Collections.Generic;
using System.Text;
using SystemApp.Models.DataModels;

namespace SystemApp.Models.ViewModels
{
	public class HomeViewModel
	{
		public IEnumerable<ProductCategory> CategoryList { get; set; }
		public IEnumerable<Product> ProductList { get; set; }
	}
}
