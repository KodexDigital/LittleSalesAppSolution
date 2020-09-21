using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SystemApp.Admin.IRepos;
using SystemApp.Admin.Repos;

namespace SystemApp.Models.DataModels
{
	public class SellerStore : BaseModel, IPseudoDeletable, IRecordStatus
	{
		public string NameOfStore { get; set; }
		public string LocationAddress { get; set; }
		public string CompanyName { get; set; }
		public string StoreUrl { get; set; }
		public bool Delete { get; set; }
		public bool IsActive { get; set; }
		public string OwnerId { get; set; }
		[ForeignKey("OwnerId")]
		public virtual ApplicationUser Owner { get; set; }
	}
}
