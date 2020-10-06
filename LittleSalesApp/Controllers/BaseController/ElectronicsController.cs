using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Repository.Admin;
using System.Threading.Tasks;
using SystemApp.Admin.Globals;
using SystemApp.Models.ViewModels;
using SystemSales.AccessLayer;

namespace LittleSalesApp.Controllers.BaseController
{
	public class ElectronicsController : Controller
	{
		private readonly ApplicationDbContext db;

		public ElectronicsController(ApplicationDbContext db)
		{
			this.db = db;
		}

		public IUnitOfWorks UnitOfWorks { get; }

		public IActionResult Index()
		{
			var productLists = db.Products.Where(x => x.CategoryId.Equals(ProductMenus.ElectronicsMenu)).ToList();
			var electronicList = new ProductServiveViewModel
			{
				ElectronicsProducts = productLists
			};
			return View(electronicList);
		}
	}
}
