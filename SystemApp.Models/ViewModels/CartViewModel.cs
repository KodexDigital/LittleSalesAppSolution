using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemApp.Models.DataModels;

namespace SystemApp.Models.ViewModels
{
	public class CartViewModel
	{
		public IList<Product> ProductServiceList { get; set; }
		public OrderHeader OrderHeader { get; set; }
		public OrderDetails OrderDetails { get; set; }
		public double TotalCost { get; set; }
		//{
		//	get { return ProductServiceList.FirstOrDefault().Price * OrderDetails.Quantity?? 1; }
		//}
	}
}
