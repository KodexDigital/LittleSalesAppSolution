using System;
using System.Collections.Generic;
using System.Linq;
using System.Repository.Admin;
using System.Threading.Tasks;
using LittleSalesApp.Extensions;
using Microsoft.AspNetCore.Mvc;
using SystemApp.Models.DataModels;
using SystemApp.Models.ViewModels;
using SystemApp.Utilities;

namespace LittleSalesApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IUnitOfWorks unitOfWorks;
        List<Guid> sessionList = new List<Guid>();

        [BindProperty]
        public CartViewModel CartViewModel { get; set; }

        public CartController(IUnitOfWorks unitOfWorks)
        {
            this.unitOfWorks = unitOfWorks;
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

        public IActionResult Summary()
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public IActionResult SummaryPOST()
        {
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
                unitOfWorks.OrderHeader.Add(CartViewModel.OrderHeader);
                unitOfWorks.Save();

                foreach (var item in CartViewModel.ProductServiceList)
                {
                    var orderDetails = new OrderDetails
                    {
                        ProductServiceId = item.Id,
                        OrderHeaderId = CartViewModel.OrderHeader.Id,
                        ServiceName = item.ProductName,
                        Price = item.Price
                    };

                    unitOfWorks.OrderDetails.Add(orderDetails);

                }
                unitOfWorks.Save();
                HttpContext.Session.SetObject(StandardVariables.SessionCart, new List<Guid>());
                return RedirectToAction("OrderConfirmation", "Cart", new { id = CartViewModel.OrderHeader.Id });
            }
        }

        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }


        public IActionResult Remove(Guid productServiceId)
        {
            sessionList = HttpContext.Session.GetObject<List<Guid>>(StandardVariables.SessionCart);
            sessionList.Remove(productServiceId);
            HttpContext.Session.SetObject(StandardVariables.SessionCart, sessionList);

            return RedirectToAction(nameof(Index));
        }
    }
}