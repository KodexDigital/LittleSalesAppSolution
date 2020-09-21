using System;
using System.Collections.Generic;
using System.Repository.Admin;
using System.Text;
using SystemApp.Models.DataModels;
using SystemSales.AccessLayer;

namespace System.Repository.Orders
{
	public class OrderDetailsRepository : DefaultRepository<OrderDetails>, IOrderDetailsRepository
	{
		private readonly ApplicationDbContext db;

		public OrderDetailsRepository(ApplicationDbContext db) : base(db)
		{
			this.db = db;
		}
	}
}
