using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SystemApp.Admin.IRepos;
using SystemApp.Admin.Repos;

namespace SystemApp.Models.DataModels
{
	public class Product : BaseModel, IPseudoDeletable, IRecordStatus
	{
		public string ProductName { get; set; }
		public string LowerCaseName { get; set; }
		public string ProductNumber { get; set; }
		public string ProductCode { get; set; }
		public Guid CategoryId { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public virtual SalesOrder SalesOrder { get; set; }
		public bool Delete { get; set; }
		public bool IsActive { get; set; }
		public string ProductImagePath { get; set; }

		public string ProductOwnerId { get; set; }
		[ForeignKey(nameof(ProductOwnerId))]
		public virtual ApplicationUser ProductOwner { get; set; }


	}
}
