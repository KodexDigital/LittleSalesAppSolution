using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Repository.Admin;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SystemApp.Admin.Constants;
using SystemApp.Logics.Helpers;
using SystemApp.Models.DataModels;
using SystemApp.Models.ViewModels;
using SystemApp.Utilities;
using SystemSales.AccessLayer;

namespace LittleSalesApp.Controllers
{
    [Authorize(Policy = "RequiredSellerPolicy")]
    public class ProductsController : Controller
    {
        private readonly IUnitOfWorks unitOfWorks;
        private readonly ApplicationDbContext context;

        [BindProperty]
        public ProductViewModel ProductVM { get; set; }
        public IWebHostEnvironment WebHostEnvironment { get; }
        public UserManager<ApplicationUser> userManager { get; }

        public ProductsController(IUnitOfWorks unitOfWorks , ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, 
            UserManager<ApplicationUser> userManager)
        {
            this.unitOfWorks = unitOfWorks;
            this.context = context;
            WebHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var appUser = await userManager.GetUserAsync(HttpContext.User);
            return View(await unitOfWorks.Products.GetAll(filter: a => a.ProductOwnerId.Equals(appUser.Id), orderBy: x => x.OrderByDescending(a => a.Id)));
        }

        public IActionResult Edit(Guid id)
        {
            ViewData["CategoryId"] = unitOfWorks.Category.GetCategoryForDropSown();
            return View(unitOfWorks.Products.Get(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product model)
        {
            if (!ModelState.IsValid)
                return View(model);
            await unitOfWorks.Products.Update(model);
            await unitOfWorks.Save();

            ViewBag.SuccessMessage = "Item updated successfully!";

            return View(model);
        }
        public IActionResult View(Guid id)
        {
            ViewData["CategoryId"] = unitOfWorks.Category.GetCategoryForDropSown();
            return View(unitOfWorks.Products.Get(id));
        }
        public IActionResult AddProduct()
        {
            ViewData["CategoryId"] = unitOfWorks.Category.GetCategoryForDropSown();
            ViewData["ProductNumber"] = CodeGenerationHelper.ProductNumber();
            ViewData["ProductCode"] = CodeGenerationHelper.ProductCode();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(ProductViewModel model)
        {
            var appUser = await userManager.GetUserAsync(HttpContext.User);
            try
            {
                if (ModelState.IsValid)
                {
                    string uniqueFileName = UploadedFile(model);

                    var storeProduct = new Product
                    {
                        ProductName = model.ProductName,
                        LowerCaseName = model.ProductName.TrimStart().Replace(" ", "-").ToLower().TrimEnd(),
                        ProductNumber = model.ProductNumber,
                        ProductCode = model.ProductCode,
                        CategoryId = model.CategoryId,
                        Description = model.Description,
                        Price = model.Price,
                        IsActive = true,
                        ProductImagePath = uniqueFileName,
                        ProductOwnerId = appUser.Id 
                    };

                    await unitOfWorks.Products.Add(storeProduct);
                    await unitOfWorks.Save();
                    return RedirectToAction(nameof(Edit), ControllerContext.RouteData.Values["controller"].ToString(), new { id = storeProduct.Id });
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message.ToString());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message.ToString());
            }
            return View(model);
        }


        private string UploadedFile(ProductViewModel model)
        {
            string uniqueFileName = null;

            if (model.FilePath != null)
            {
                string uploadsFolder = Path.Combine(WebHostEnvironment.WebRootPath, GlobalNames.ProductImagePath);
                uniqueFileName = Guid.NewGuid().ToString() + GlobalNames.Seperaotor + model.FilePath.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                model.FilePath.CopyTo(fileStream);
            }
            return uniqueFileName;
        }

    }
}