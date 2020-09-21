using System;
using System.Collections.Generic;
using System.Linq;
using System.Repository.Admin;
using System.Text;
using System.Threading.Tasks;
using SystemApp.Models.DataModels;
using SystemSales.AccessLayer;

namespace System.Repository.Orders
{
	public class OrderHeaderRepository : DefaultRepository<OrderHeader>, IOrderHeaderRepository
	{
		private new readonly ApplicationDbContext context;
		public OrderHeaderRepository(ApplicationDbContext context) : base(context)
		{
			this.context = context;
		}
		public async Task ChangeOrderStatus(Guid orderHeaderId, string status)
		{
			var orderFromDb = context.OrderHeaders.FirstOrDefault(o => o.Id.Equals(orderHeaderId));
			orderFromDb.Status = status;
			await context.SaveChangesAsync();
		}
	}
}
