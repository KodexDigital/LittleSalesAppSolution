using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Repository.Admin;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SystemApp.Admin.Constants;
using SystemApp.Logics.Helpers;
using SystemApp.Models.DataModels;
using SystemApp.Models.ViewModels;
using SystemSales.AccessLayer;

namespace LittleSalesApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IUnitOfWorks unitOfWorks;
        private readonly ApplicationDbContext context;

        [BindProperty]
        public ProductViewModel ProductVM { get; set; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public ProductsController(IUnitOfWorks unitOfWorks , ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWorks = unitOfWorks;
            this.context = context;
            WebHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            return View(await unitOfWorks.Products.GetAll());
        }

        public IActionResult Edit(Guid id)
        {
            return View(unitOfWorks.Category.Get(id));
        }

        public IActionResult AddProduct()
        {
            ViewData["CategoryId"] = new SelectList(context.ProductCategories, "Id", "CategoryName");
            ViewData["ProductNumber"] = CodeGenerationHelper.ProductNumber();
            ViewData["ProductCode"] = CodeGenerationHelper.ProductCode();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = ActionRoleLevels.SellerRole)]
        public async Task<IActionResult> AddProduct(ProductViewModel model)
        {
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
                        Category = model.Category,
                        Description = model.Description,
                        Price = model.Price,
                        IsActive = true,
                        ProductImagePath = uniqueFileName,
                    };

                    await unitOfWorks.Products.Add(storeProduct);
                    await unitOfWorks.Save();
                    return RedirectToAction(nameof(Edit), ControllerContext.RouteData.Values["controller"].ToString(), new { id = model.Id });
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