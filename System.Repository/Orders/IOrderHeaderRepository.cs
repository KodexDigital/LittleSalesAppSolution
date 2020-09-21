using System;
using System.Collections.Generic;
using System.Repository.Admin;
using System.Text;
using System.Threading.Tasks;
using SystemApp.Models.DataModels;

namespace System.Repository.Orders
{
	public interface IOrderHeaderRepository : IDefaultRepository<OrderHeader>
	{
		Task ChangeOrderStatus(Guid orderHeaderId, string status);
	}
}
