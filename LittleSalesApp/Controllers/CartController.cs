﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Repository.Admin;
using System.Threading.Tasks;
using LittleSalesApp.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SystemApp.Logics.BusinessLogics;
using SystemApp.Logics.Helpers;
using SystemApp.Models.Carts;
using SystemApp.Models.DataModels;
using SystemApp.Models.ViewModels;
using SystemApp.Utilities;
using SystemSales.AccessLayer;

namespace LittleSalesApp.Controllers
{

    public class CartController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IUnitOfWorks unitOfWorks;
        private readonly UserManager<ApplicationUser> userManager;
        List<Guid> sessionList = new List<Guid>();

        [BindProperty]
        public CartViewModel CartViewModel { get; set; }

        public CartController(ApplicationDbContext context, IUnitOfWorks unitOfWorks, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.unitOfWorks = unitOfWorks;
            this.userManager = userManager;
            CartViewModel = new CartViewModel
            {
                ProductServiceList = new List<Product>(),
                OrderHeader = new OrderHeader()
            };
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetObject<List<Guid>>(StandardVariables.SessionCart) != null)
            {
                sessionList = HttpContext.Session.GetObject<List<Guid>>(StandardVariables.SessionCart);
                foreach (Guid productServiceId in sessionList)
                {
                    CartViewModel.ProductServiceList.Add(unitOfWorks.Products.GetFirstOrDefault(p => p.Id.Equals(productServiceId)));
                }
            }
            return View(CartViewModel);
        }

        public IActionResult Buy(Guid prodcutId)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(StandardVariables.SessionCart)))
            {
                sessionList.Add(prodcutId);
                HttpContext.Session.SetObject(StandardVariables.SessionCart, sessionList);
            }
            else
            {
                sessionList = HttpContext.Session.GetObject<List<Guid>>(StandardVariables.SessionCart);
                if (!sessionList.Contains(prodcutId))
                {
                    sessionList.Add(prodcutId);
                    HttpContext.Session.SetObject(StandardVariables.SessionCart, sessionList);
                }
            }

            return RedirectToAction(nameof(Index),"Home");
            //return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(Guid prodcutId)
        {
            sessionList = HttpContext.Session.GetObject<List<Guid>>(StandardVariables.SessionCart);
            sessionList.Remove(prodcutId);
            HttpContext.Session.SetObject(StandardVariables.SessionCart, sessionList);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ActionName(nameof(Index))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder()
        {
            var appUser = await userManager.GetUserAsync(HttpContext.User);

            if (HttpContext.Session.GetObject<List<Guid>>(StandardVariables.SessionCart) != null)
            {
                sessionList = HttpContext.Session.GetObject<List<Guid>>(StandardVariables.SessionCart);
                CartViewModel.ProductServiceList = new List<Product>();
                foreach (Guid productServiceId in sessionList)
                {
                    CartViewModel.ProductServiceList.Add(unitOfWorks.Products.Get(productServiceId));
                }
            }

            if (!ModelState.IsValid)
            {
                return View(CartViewModel);
            }
            else
            {
                CartViewModel.OrderHeader.OrderDate = DateTime.Now;
                CartViewModel.OrderHeader.Status = StandardVariables.StatusSubmitted;
                CartViewModel.OrderHeader.ProductServiceCount = CartViewModel.ProductServiceList.Count;
                CartViewModel.OrderHeader.Total = CartViewModel.TotalCost;
                await unitOfWorks.OrderHeader.Add(CartViewModel.OrderHeader);
                await unitOfWorks.Save();

                foreach (var item in CartViewModel.ProductServiceList)
                {
                    var orderDetails = new OrderDetails
                    {
                        ProductServiceId = item.Id,
                        OrderHeaderId = CartViewModel.OrderHeader.Id,
                        ServiceName = item.ProductName,
                        UnitPrice = item.Price,
                        Quantity = CartViewModel.OrderDetails.Quantity,
                        CustomerId = appUser.Id ?? null
                    };

                    await unitOfWorks.OrderDetails.Add(orderDetails);

                }
                await unitOfWorks.Save();
                HttpContext.Session.SetObject(StandardVariables.SessionCart, new List<Guid>());
                return RedirectToAction("OrderConfirmation", "Cart", new { id = CartViewModel.OrderHeader.Id });
            }
        }

        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }




        #region Check existing item in cart
        private int IsItemExist(Guid id)
        {
            List<CartItems> CartItems = SessionHelper.GetObjectFromJson<List<CartItems>>(HttpContext.Session, StandardVariables.SessionCart);
            for(int c = 0; c < CartItems.Count; c++)
            {
                if (CartItems[c].ProductServiceList.FirstOrDefault().Id.Equals(id))
                    return 1;
            }
            return -1;
        }
        #endregion
    }
}