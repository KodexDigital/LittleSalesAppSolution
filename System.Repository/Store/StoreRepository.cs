using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Repository.Admin;
using System.Text;
using System.Threading.Tasks;
using SystemApp.Models.DataModels;
using SystemApp.Models.ViewModels;
using SystemSales.AccessLayer;

namespace System.Repository.Store
{
	public class StoreRepository : DefaultRepository<SellerStore>, IStoreRepository
	{
		private readonly ApplicationDbContext db;

		public StoreRepository(ApplicationDbContext db) : base(db)
		{
			this.db = db;
		}
		public IEnumerable<SelectListItem> GetStoreForDropDown()
		{
			return db.SellerStores.Select(s => new SelectListItem { Text = s.NameOfStore, Value = s.Id.ToString() });
		}

		public async Task Update(SellerStore store)
		{
			var objFromDb = db.SellerStores.FirstOrDefault(s => s.Id.Equals(store.Id));

			objFromDb.NameOfStore = store.NameOfStore;
			objFromDb.CompanyName = store.CompanyName;
			objFromDb.LocationAddress = store.LocationAddress;
			objFromDb.IsActive = store.IsActive;
			objFromDb.StoreUrl = store.StoreUrl;
			objFromDb.CreatedAt = store.CreatedAt;
			await db.SaveChangesAsync();
		}
	}
}
