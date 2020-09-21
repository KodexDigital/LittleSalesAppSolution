﻿using System;
using System.Collections.Generic;
using System.Text;
using SystemApp.Models.DataModels;

namespace SystemApp.Models.ViewModels
{
	public class CartViewModel
	{
		public IList<Product> ProductServiceList { get; set; }
		public OrderHeader OrderHeader { get; set; }
	}
}
