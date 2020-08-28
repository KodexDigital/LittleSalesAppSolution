using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SystemApp.Admin.Repos;

namespace SystemApp.Models.DataModels
{
	public class SalesOrder : BaseModel
	{
		public string OrdeNumber { get; set; }
		public Guid ProductId { get; set; }
		[ForeignKey("Product")]
		public ICollection<Product> Products { get; set; }

		public SalesOrder()
		{
			Products = new HashSet<Product>();
		}
	}
}
