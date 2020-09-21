using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemApp.Models.DataModels;

namespace LittleSalesApp.TagHelper
{
	[HtmlTargetElement("td", Attributes = "i-role")]
	public class RoleUsersTagHelper
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;

		public RoleUsersTagHelper(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			this.userManager = userManager;
			this.roleManager = roleManager;
		}

		[HtmlAttributeName("i-role")]
		public string Role { get; set; }

		public async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			List<string> names = new List<string>();
			IdentityRole role = await roleManager.FindByIdAsync(Role);
			if (role != null)
			{
				foreach (var user in userManager.Users)
				{
					if (user != null && await userManager.IsInRoleAsync(user, role.Name))
						names.Add(user.UserName);
				}
			}
			output.Content.SetContent(names.Count == 0 ? "No Users" : string.Join(", ", names));
		}
	}
}
