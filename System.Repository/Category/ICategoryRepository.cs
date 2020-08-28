using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Repository.Admin;
using System.Text;
using System.Threading.Tasks;
using SystemApp.Models.DataModels;

namespace System.Repository.Category
{
	public interface ICategoryRepository : IDefaultRepository<ProductCategory>
	{
		IEnumerable<SelectListItem> GetCategoryForDropSown();
		Task Update(ProductCategory category);
	}
}
