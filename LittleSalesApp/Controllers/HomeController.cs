﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LittleSalesApp.Models;
using SystemApp.Models.ViewModels;
using System.Repository.Admin;
using Microsoft.AspNetCore.Http;
using SystemApp.Utilities;
using LittleSalesApp.Extensions;
using SystemApp.Models.DataModels;
using SystemSales.AccessLayer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LittleSalesApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWorks unitOfWorks;
		private readonly ApplicationDbContext context;
		List<Guid> sessionList = new List<Guid>();

		[BindProperty]
		public HomeViewModel homeViewModel { get; set; }

		public HomeController(ILogger<HomeController> logger, IUnitOfWorks unitOfWorks, ApplicationDbContext context)
		{
			_logger = logger;
			this.unitOfWorks = unitOfWorks;
			this.context = context;
		}

		public async Task<IActionResult> Index()
		{

			var products = await unitOfWorks.Products.GetAll();
			var mainMenu = await context.ProductCategories.Distinct().ToListAsync();
			var homeVM = new HomeViewModel
			{
				ProductList = products,
				CategoryList = mainMenu
			};
			return View(homeVM);
		}
		public IActionResult ProductDetails(Guid productId)
		{
			var singleProduct = unitOfWorks.Products.GetFirstOrDefault(filter: p => p.Id.Equals(productId));
			var otherProduct = context.Products.Where(x => !x.Id.Equals(productId)).ToList();
			var productLists = new ProductDetailsViewModel
			{
				ProductList = otherProduct,
				SingleProduct = singleProduct
			};
			return View(productLists);
		}

		public IActionResult AddToCart(Guid productId)
		{
			if (string.IsNullOrEmpty(HttpContext.Session.GetString(StandardVariables.SessionCart)))
			{
				sessionList.Add(productId);
				HttpContext.Session.SetObject(StandardVariables.SessionCart, sessionList);
			}
			else
			{
				sessionList = HttpContext.Session.GetObject<List<Guid>>(StandardVariables.SessionCart);
				if (!sessionList.Contains(productId))
				{
					sessionList.Add(productId);
					HttpContext.Session.SetObject(StandardVariables.SessionCart, sessionList);
				}
			}

			return RedirectToAction(nameof(Index));
		}
		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult Test()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
