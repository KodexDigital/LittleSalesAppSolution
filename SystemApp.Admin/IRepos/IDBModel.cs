using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SystemApp.Admin.IRepos
{
	public interface IDBModel
	{
		[Key]
		Guid Id { get; set; }
		DateTime CreatedAt { get; set; }
	}
}
