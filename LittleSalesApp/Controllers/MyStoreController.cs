using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SystemApp.Models.DataModels;
using SystemApp.Models.ViewModels;

namespace LittleSalesApp.Controllers
{
	public class MyStoreController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;

		public MyStoreController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}
		public IActionResult Index()
		{
			return View();
		}

		[Route("customer-store/{id}", Name = "UserStore")]
		public IActionResult CustomerStore()
		{
			return View();
		}

		public async Task<IActionResult> StoreAuth(string returnUrl = null)
		{
			if(returnUrl == null)
				await signInManager.SignOutAsync();

			var loginModel = new LoginViewModel
			{
				ReturnUrl = returnUrl
			};
			return View(loginModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> StoreAuth(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var appUser = await userManager.FindByEmailAsync(model.Email);
				if (appUser != null)
				{
					await signInManager.SignOutAsync();
					Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser, model.Password, isPersistent: true, false);
					if (result.Succeeded)
					{
						if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
							return Redirect(model.ReturnUrl ?? "/");
						else
							return RedirectToAction("Create", "Store");
					}

				}
				ModelState.AddModelError(nameof(model.Email), "Login Failed: Invalid login attempt");
			}
			return View(model);
		}

	}
}