using System;
using System.Collections.Generic;
using System.Text;

namespace SystemApp.Models.Carts
{
	public class PaymentIcons
	{
		public int ID { get; set; }
		public string IconName { get; set; }
		public string IconBgColor { get; set; }
		public string IconClass { get; set; }

		public static List<PaymentIcons> SupportedPayments()
		{
			List<PaymentIcons> icons = new List<PaymentIcons>
			{
				 new PaymentIcons{ID = 1, IconName = "MasterCard", IconClass="fab fa-cc-mastercard", IconBgColor="#eb001b"},
				 new PaymentIcons{ID = 2, IconName = "Visa", IconClass="fab fa-cc-visa", IconBgColor="#1A1F71"},
				 new PaymentIcons{ID = 3, IconName = "PayPal", IconClass="fab fa-cc-paypal", IconBgColor="#3b7bbf"},
				 new PaymentIcons{ID = 4, IconName = "Stripe", IconClass="fab fa-cc-stripe", IconBgColor="#008cdd"},
				 new PaymentIcons{ID = 5, IconName = "BitCoin", IconClass="fab fa-bitcoin", IconBgColor="#f7931a"},
				 new PaymentIcons{ID = 6, IconName = "Ethereum", IconClass="fab fa-ethereum", IconBgColor="#3c3c3d"},
			};
			return icons;
		}
	}
}
