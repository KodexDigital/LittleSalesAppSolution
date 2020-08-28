using System;
using System.Collections.Generic;
using System.Text;
using SystemApp.Admin.Repos;

namespace SystemApp.Models.DataModels
{
	public class ProductSalesDetails : BaseModel
	{
		public virtual ICollection<Product> Products { get; set; }
		public virtual ICollection<SalesOrder> SalesOrders { get; set; }


		public ProductSalesDetails()
		{
			SalesOrders = new HashSet<SalesOrder>();
			Products = new HashSet<Product>();
		}
	}
}
