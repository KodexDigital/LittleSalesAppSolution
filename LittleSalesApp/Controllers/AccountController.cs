using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LittleSalesApp.TagHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SystemApp.Admin.Constants;
using SystemApp.Models.DataModels;
using SystemApp.Models.ViewModels;
using SystemApp.Utilities;

namespace LittleSalesApp.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly IWebHostEnvironment webHostEnvironment;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
			RoleManager<IdentityRole> roleManager,
			IWebHostEnvironment webHostEnvironment)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.roleManager = roleManager;
			this.webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			return View();
		}

		[AllowAnonymous]
		public IActionResult Register() => View();

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				string uniqueFileName = UploadProfileHelper(model);

				var user = new ApplicationUser 
				{ 
					Name = model.Name,
					Email = model.Email,
					UserName = model.UserName,
					PhoneNumber = model.PhoneNumber,
					PostalCode = model.PostalCode,
					State = model.State,
					City = model.City,
					StreetAddress = model.StreetAddress,
					UserImagePath = uniqueFileName
				};

				var result = await userManager.CreateAsync(user, model.PasswordHash);
				if (result.Succeeded)
				{
					var userRole = new IdentityRoleHelper(roleManager, userManager);
					await userRole.CreateNewUserRoleAsnc(StandardVariables.UserRole);
					await userManager.AddToRoleAsync(user, StandardVariables.UserRole);


					//if (signInManager.IsSignedIn(User) && User.IsInRole(StandardVariables.AdminRole))
					//	return RedirectToAction("Index", "AdminPage");

					//if (signInManager.IsSignedIn(User) && User.IsInRole(StandardVariables.SellerRole))
					//	return RedirectToAction("Index", "SellerPage");

					await signInManager.SignInAsync(user, isPersistent: false);
					return RedirectToAction("Index", "Home");
				}
				foreach (var errors in result.Errors)
					ModelState.AddModelError(string.Empty, errors.Description);
			}
			return View(model);
		}

		[AllowAnonymous]
		public IActionResult Login(string returnUrl = null)
		{
			var loginModel = new LoginViewModel
			{
				ReturnUrl = returnUrl
			};
			return View(loginModel);
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var appUser = await userManager.FindByEmailAsync(model.Email);
				if (appUser != null)
				{
					await signInManager.SignOutAsync();
					Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser, model.Password, false, false);
					if (result.Succeeded)
					{
						if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
							return Redirect(model.ReturnUrl); // or return Redirect(model.ReturnUrl ?? "/");
						else
							return RedirectToAction("Index", "Home");
					}
					
				}
				ModelState.AddModelError(nameof(model.Email), "Login Failed: Invalid login attempt");
			}
			return View(model);

			//if (ModelState.IsValid)
			//{
			//	var result = await signInManager.PasswordSignInAsync(model.Username,
			//	   model.Password, model.RememberMe, lockoutOnFailure: true);

			//	if (result.Succeeded)
			//	{
			//		if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
			//			return Redirect(model.ReturnUrl);
			//		else
			//			return RedirectToAction("Index", "Home");
			//	}
			//	if (result.Succeeded)
			//	{
			//		_logger.LogInformation("User logged in.");
			//		return LocalRedirect(model.ReturnUrl);
			//	}
			//	if (result.RequiresTwoFactor)
			//	{
			//		return RedirectToPage("./LoginWith2fa", new
			//		{
			//			model.ReturnUrl,
			//			model.RememberMe
			//		});
			//	}
			//	if (result.IsLockedOut)
			//	{
			//		_logger.LogWarning("User account locked out.");
			//		return RedirectToPage("./Lockout");
			//	}
			//	else
			//	{
			//		ModelState.AddModelError(string.Empty, "Invalid login attempt.");
			//		return View(model);
			//	}
			//}
			//ModelState.AddModelError(string.Empty, "Invalid login attempt");
			//return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Logout(string returnUrl = null)
		{
			await signInManager.SignOutAsync();
			if (returnUrl != null)
				return LocalRedirect(returnUrl);
			else
				return RedirectToAction("index", "Home");
		}

		public IActionResult AccessDenied() => View();


		#region FileUPload Function
		private string UploadProfileHelper(RegisterViewModel model)
		{
			string uniqueFileName = null;
			if(model.ProfileImageFilePath != null)
			{
				string folderPath = Path.Combine(webHostEnvironment.WebRootPath, GlobalNames.ProfileImagePath);
				uniqueFileName = Guid.NewGuid().ToString() + GlobalNames.Seperaotor + model.ProfileImageFilePath.FileName;
				string filePath = Path.Combine(folderPath, uniqueFileName);
				using var fileStream = new FileStream(filePath, FileMode.Create);
				model.ProfileImageFilePath.CopyTo(fileStream);
			}
			return uniqueFileName;
		}
		#endregion
	}
}