using System;
using System.Collections.Generic;
using System.Text;

namespace SystemApp.Models.DataModels
{
	public class Checkout : OrderDetails
	{
		public string PaymentType { get; set; }
		public string ViaThirdParty { get; set; }
	}
}
