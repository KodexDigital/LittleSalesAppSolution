using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Repository.Admin;
using System.Text;
using System.Threading.Tasks;
using SystemApp.Models.DataModels;
using SystemApp.Models.ViewModels;

namespace System.Repository.Store
{
	public interface IStoreRepository : IDefaultRepository<SellerStore>
	{
		IEnumerable<SelectListItem> GetStoreForDropDown();
		Task Update(SellerStore store);
	}
}
