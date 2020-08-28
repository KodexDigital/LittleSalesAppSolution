using System;
using System.Collections.Generic;
using System.Text;
using SystemApp.Admin.Constants;
using SystemApp.Admin.IRepos;

namespace SystemApp.Admin.Repos
{
	public class BaseModel : IDBModel
	{
		public Guid Id { get; set; }
		public DateTime CreatedAt { get; set; }

		public BaseModel()
		{
			CreatedAt = DateTime.Now;
		}
	}
}
