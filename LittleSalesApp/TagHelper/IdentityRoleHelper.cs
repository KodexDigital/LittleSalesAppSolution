using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SystemApp.Models.DataModels;
using SystemApp.Utilities;

namespace LittleSalesApp.TagHelper
{
	public class IdentityRoleHelper : GenericIdentityRoleHelper
	{
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly UserManager<ApplicationUser> userManager;

		public IdentityRoleHelper(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
		{
			this.roleManager = roleManager;
			this.userManager = userManager;
		}
		public async override Task CreateNewUserRoleAsnc([Required] string roleName)
		{
			if (!await roleManager.RoleExistsAsync(roleName))
				await roleManager.CreateAsync(new IdentityRole(roleName));
		}

		public async override Task UpdateExistingUserRoleAsync(ApplicationUser user, [Required] string newRoleName)
		{
			if (user != null)
			{
				IdentityResult result = await userManager.RemoveFromRoleAsync(user, StandardVariables.UserRole);
				if (result.Succeeded)
				{
					await CreateNewUserRoleAsnc(newRoleName);
					await userManager.AddToRoleAsync(user, newRoleName);
				}
			}
		}
	}
}
