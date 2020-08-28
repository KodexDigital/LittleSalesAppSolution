using System;
using System.Collections.Generic;
using System.Linq;
using System.Repository.Admin;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SystemApp.Models.DataModels;
using SystemApp.Models.ViewModels;
using SystemSales.AccessLayer;

namespace LittleSalesApp.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly IUnitOfWorks unitOfWorks;
        private readonly ApplicationDbContext context;

        public ProductCategoryController(IUnitOfWorks unitOfWorks, ApplicationDbContext context)
        {
            this.unitOfWorks = unitOfWorks;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await unitOfWorks.Category.GetAll());
        }

        public IActionResult Edit(Guid id)
        {
            return View(unitOfWorks.Category.Get(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductCategory category)
        {
            if (!ModelState.IsValid)
                return View(category);
            await unitOfWorks.Category.Update(category);
            await unitOfWorks.Save();

            ViewBag.SuccessMessage = "Item updated successfully!";

            return View(category);
        }

        public IActionResult View(Guid id)
        {
            return View(unitOfWorks.Category.Get(id));
        }
        public IActionResult AddCategory() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(ProductCategoryViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var category = new ProductCategory
                    {
                        CategoryName = viewModel.CategoryName,
                        LowerCaseName = viewModel.CategoryName.TrimStart().Replace(" ", "-").ToLower().TrimEnd(),
                        ShortNote = viewModel.ShortNote,
                        OderPosition = viewModel.OderPosition,
                        IsActive = true,
                        Delete = false
                    };
                    await unitOfWorks.Category.Add(category);
                    await unitOfWorks.Save();
                    return RedirectToAction("Edit", "ProductCategory", new { id = category.Id });
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
            return View(viewModel);

            //return RedirectToAction("Edit", this.ViewContext.RouteData.Values["controller"].ToString(), new { id = category.Id });
            //return RedirectToAction("Edit", this.ViewContext.RouteData.Values["controller"].ToString(), new { id = category.Id });
        }

        public async Task<IActionResult> InActiveCategory()
        {
            return View(await unitOfWorks.Category.GetAll(filter: a => !a.IsActive));
        }
    }
}