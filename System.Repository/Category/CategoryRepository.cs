using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Repository.Admin;
using System.Text;
using System.Threading.Tasks;
using SystemApp.Models.DataModels;
using SystemSales.AccessLayer;

namespace System.Repository.Category
{
	public class CategoryRepository : DefaultRepository<ProductCategory>, ICategoryRepository
	{
		private readonly ApplicationDbContext db;

		public CategoryRepository(ApplicationDbContext db) : base(db)
		{
			this.db = db;
		}
		public IEnumerable<SelectListItem> GetCategoryForDropSown()
		{
			return db.ProductCategories.Select(c => new SelectListItem() { Text = c.CategoryName, Value = c.Id.ToString() });
		}

		public async Task Update(ProductCategory category)
		{
			var objFromDb = db.ProductCategories.FirstOrDefault(s => s.Id == category.Id);

			objFromDb.CategoryName = category.CategoryName;
			objFromDb.LowerCaseName = category.CategoryName.TrimStart().Replace(" ", "-").ToLower().TrimEnd();
			objFromDb.ShortNote = category.ShortNote;
			objFromDb.OderPosition = category.OderPosition;
			objFromDb.IsActive = category.IsActive;
			objFromDb.CreatedAt = category.CreatedAt;
			await db.SaveChangesAsync();
		}
	}
}
