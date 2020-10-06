using System;
using System.Collections.Generic;
using System.Linq;
using System.Repository.Admin;
using System.Threading.Tasks;
using LittleSalesApp.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SystemApp.Models.DataModels;
using SystemApp.Models.ViewModels;
using SystemApp.Utilities;

namespace LittleSalesApp.Controllers
{
    //[Authorize]
    public class CheckoutController : Controller
    {
        private readonly IUnitOfWorks unitOfWorks;
        private readonly UserManager<ApplicationUser> userManager;
        private string CurrentController = "";
        List<Guid> sessionList = new List<Guid>();

        [BindProperty]
        public CartViewModel CartViewModel { get; set; }

        public CheckoutController(IUnitOfWorks unitOfWorks, UserManager<ApplicationUser> userManager)
        {
            this.unitOfWorks = unitOfWorks;
            this.userManager = userManager;

        }

        [Route("Cart/Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Cart/Index")]
        public IActionResult AddressAndPayment()
        {
            //CartViewModel cartItems = TempData["CartItems"] as CartViewModel;
            return View();
        }

        [HttpPost]
        [ActionName(nameof(AddressAndPayment))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddressPayment()
        {
            //CurrentController = ControllerContext.RouteData.Values["controller"].ToString();
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
                await unitOfWorks.OrderHeader.Add(CartViewModel.OrderHeader);
                await unitOfWorks.Save();

                foreach (var item in CartViewModel.ProductServiceList)
                {
                    var orderDetails = new Checkout
                    {
                        ProductServiceId = item.Id,
                        OrderHeaderId = CartViewModel.OrderHeader.Id,
                        ServiceName = item.ProductName,
                        UnitPrice = item.Price,
                        CustomerId = appUser.Id
                    };

                    await unitOfWorks.OrderDetails.Add(orderDetails);

                }
                await unitOfWorks.Save();
                HttpContext.Session.SetObject(StandardVariables.SessionCart, new List<Guid>());
                return RedirectToAction("CheckoutConfirm", "Checkout", new { id = CartViewModel.OrderHeader.Id });
            }
        }

        public IActionResult CheckoutConfirm(int id)
        {
            return View(id);
        }
    }
}