using System;
using System.Collections.Generic;
using System.Text;
using SystemApp.Admin.IRepos;
using SystemApp.Admin.Repos;

namespace SystemApp.Models.DataModels
{
	public class ProductCategory : BaseModel, IRecordStatus, IPseudoDeletable
	{
		public string CategoryName { get; set; }
		public string LowerCaseName { get; set; }
		public string ShortNote { get; set; }
		public int OderPosition { get; set; }
		public bool IsActive { get; set; }
		public bool Delete { get; set; }
	}
}
