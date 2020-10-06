using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemApp.Models.Carts;

namespace LittleSalesApp.ViewComponents
{
	public class CheckoutPaymentSupportViewComponent : ViewComponent
	{
		List<PaymentIcons> paymentIcons = new List<PaymentIcons>();
		public CheckoutPaymentSupportViewComponent()
		{
			paymentIcons = PaymentIcons.SupportedPayments();
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var model = paymentIcons;
			return await Task.FromResult((IViewComponentResult)View("CheckoutPaymentSupport", model));
		}
	}
}
