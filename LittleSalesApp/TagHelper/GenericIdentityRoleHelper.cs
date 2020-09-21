using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SystemApp.Models.DataModels;

namespace LittleSalesApp.TagHelper
{
	public abstract class GenericIdentityRoleHelper
	{
		public abstract Task CreateNewUserRoleAsnc([Required]string roleName);
		public abstract Task UpdateExistingUserRoleAsync(ApplicationUser user, [Required]string newRoleName);
	}
}
