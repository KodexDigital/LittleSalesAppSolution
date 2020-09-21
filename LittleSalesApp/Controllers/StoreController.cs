using System;
using System.Collections.Generic;
using System.Linq;
using System.Repository.Admin;
using System.Threading.Tasks;
using LittleSalesApp.TagHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SystemApp.Models.DataModels;
using SystemApp.Models.ViewModels;
using SystemApp.Utilities;
using SystemSales.AccessLayer;

namespace LittleSalesApp.Controllers
{
    [Authorize(Policy = "UserSellerPolicy")]
    public class StoreController : Controller
    {
        private readonly IUnitOfWorks unitOfWorks;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        private string ControllerName { get; set; }
        private string ActionrName { get; set; }
        private string RouteParams { get; set; }

        public StoreController(IUnitOfWorks unitOfWorks, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.unitOfWorks = unitOfWorks;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            ControllerName = "MyStore";
            ActionrName = "CustomerStore/";
            RouteParams = string.Empty;
            string currentController = ControllerContext.RouteData.Values["controller"].ToString();
            string currentAction = ControllerContext.RouteData.Values["action"].ToString();
            var storeController = HttpContext.Request.GetDisplayUrl().Replace(currentController, ControllerName)
                                                                     .Replace(currentAction, ActionrName + RouteParams).ToString();

            ViewData["StoreUrl"] = storeController;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SellerStoreViewModel model)
        {
            var appUser = await userManager.GetUserAsync(HttpContext.User);

            ControllerName = "MyStore";
            ActionrName = "CustomerStore/";
            RouteParams = model.NameOfStore.Replace(" ", "-");
            string currentController = ControllerContext.RouteData.Values["controller"].ToString();
            string currentAction = ControllerContext.RouteData.Values["action"].ToString();
            var storeController = HttpContext.Request.GetDisplayUrl().Replace(currentController, ControllerName)
                                                                     .Replace(currentAction, ActionrName + RouteParams).ToString();
            if (ModelState.IsValid)
            {
                ViewData["StoreUrl"] = storeController;
                var sellerData = new SellerStore
                {
                    NameOfStore = model.NameOfStore,
                    LocationAddress = model.LocationAddress,
                    CompanyName = model.CompanyName,
                    StoreUrl = storeController,
                    IsActive = true,
                    OwnerId = appUser.Id
                };

                await unitOfWorks.Store.Add(sellerData);
                await unitOfWorks.Save();
                TempData["StoreUrl"] = sellerData.StoreUrl;
                var newRole = new IdentityRoleHelper(roleManager, userManager);
                await newRole.UpdateExistingUserRoleAsync(appUser, StandardVariables.SellerRole);
                //await userManager.AddToRoleAsync(appUser, StandardVariables.SellerRole);
                //ChangeCurrentUserRoleToSeller(appUser, StandardVariables.SellerRole);
                //if (appUser != null)
                //{
                //    IdentityResult result = await userManager.RemoveFromRoleAsync(appUser, StandardVariables.UserRole);
                //    if (!result.Succeeded)
                //        Errors(result);
                //    var newRole = new IdentityRoleHelper(roleManager, userManager);
                //    await newRole.CreateNewUserRoleAsnc(StandardVariables.SellerRole);
                //    await userManager.AddToRoleAsync(appUser, StandardVariables.SellerRole);
                //}
                ViewBag.SuccessMessage = "Your store was created successfully";
            }
            foreach (var errors in ModelState)
            {
                ModelState.AddModelError(string.Empty, errors.ToString());
            }

            return View(model);
        }

        public IActionResult Edit(Guid id)
        {
            return View(unitOfWorks.Store.Get(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SellerStore model)
        {
            if (!ModelState.IsValid)
                return View(model);
            await unitOfWorks.Store.Update(model);
            await unitOfWorks.Save();

            ViewBag.SuccessMessage = "Store modified successfully!";

            return View(model);
        }


        #region Model state validation
        public void Errors(IdentityResult result)
        {
            foreach (IdentityError errors in result.Errors)
                ModelState.AddModelError(string.Empty, errors.Description);
        }
        #endregion



        #region Role Managing
        //public async void ChangeCurrentUserRoleToSeller(ApplicationUser appUser, string newRoleName)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (appUser != null)
        //        {
        //            IdentityResult result = await userManager.RemoveFromRoleAsync(appUser, StandardVariables.UserRole);
        //            if (!result.Succeeded)
        //                Errors(result);
        //            var newRole = new IdentityRoleHelper(roleManager);
        //            await newRole.CreateNewUserRoleAsnc(newRoleName);
        //            await userManager.AddToRoleAsync(appUser, newRoleName);
        //        }
        //    }
        //}
        #endregion
    }
}