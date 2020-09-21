using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SystemApp.Models.DataModels;

namespace SystemApp.Models.ViewModels
{
	public class SellerStoreViewModel
	{
		public SellerStoreViewModel()
		{
		}
		public SellerStoreViewModel(SellerStore store)
		{
			store.NameOfStore = NameOfStore;
			store.LocationAddress = LocationAddress;
			store.CompanyName = CompanyName;
			store.StoreUrl = StoreUrl;
			store.IsActive = true;
			store.OwnerId = OwnerId;
		}

		[Key]
		public Guid Id { get; set; }
		[Required(ErrorMessage ="Enter the name of your store")]
		[Display(Name ="Name of store")]
		public string NameOfStore { get; set; }

		[Required(ErrorMessage = "Enter the store address")]
		[Display(Name = "Store address")]
		public string LocationAddress { get; set; }

		[Required(ErrorMessage = "Enter company's name")]
		[Display(Name = "Store company's name")]
		public string CompanyName { get; set; }

		[Display(Name = "Store link")]
		public string StoreUrl { get; set; }

		public bool Delete { get; set; }

		[Display(Name = "Active")]
		public bool IsActive { get; set; }

		[Display(Name = "Store owner")]
		public string OwnerId { get; set; }
	}
}
