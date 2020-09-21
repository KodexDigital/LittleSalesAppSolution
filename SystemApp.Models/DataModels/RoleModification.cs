using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SystemApp.Models.DataModels
{
	public class RoleModification
	{

		public string RoleId { get; set; }

		[Required]
		public string RoleName { get; set; }

		public string[] AddIds { get; set; }

		public string[] DeleteIds { get; set; }
	}
}
