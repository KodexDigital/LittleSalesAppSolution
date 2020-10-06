using System;
using System.Collections.Generic;
using System.Text;
using SystemApp.Admin.Repos;
using SystemApp.Models.DataModels;

namespace SystemApp.Models.Carts
{
	public class CartItems : BaseModel
	{
		public double Quantity { get; set; }
		//public Product Product { get; set; }
		public IEnumerable<Product> ProductServiceList { get; set; }
	}
}
