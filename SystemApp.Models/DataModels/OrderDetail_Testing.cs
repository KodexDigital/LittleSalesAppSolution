using System;
using System.Collections.Generic;
using System.Text;
using SystemApp.Admin.Repos;

namespace SystemApp.Models.DataModels
{
	public class OrderDetail_Testing : BaseModel
	{
		public int OrderId { get; set; }

		public string Username { get; set; }

		public int ProductId { get; set; }

		public int Quantity { get; set; }

		public double? UnitPrice { get; set; }
	}
}
