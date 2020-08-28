using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SystemApp.Models.DataModels;

namespace SystemApp.Models.ViewModels
{
	public class ProductCategoryViewModel
	{
		public ProductCategoryViewModel()
		{

		}
		public ProductCategoryViewModel(ProductCategory categoryMapping)
		{
			categoryMapping.CategoryName = CategoryName;
			categoryMapping.LowerCaseName = LowerCaseName;
			categoryMapping.ShortNote = ShortNote;
			categoryMapping.OderPosition = OderPosition;
			categoryMapping.CreatedAt = CreatedAt;
		}

		public Guid Id { get; set; }

		[Required(ErrorMessage ="Please enter the category name")]
		[Display(Name ="Category name")]
		public string CategoryName { get; set; }

		[Display(Name = "Normalized name")]
		public string LowerCaseName { get; set; }

		[Display(Name = "Short note")]
		[MaxLength(200)]
		[DataType(DataType.MultilineText)]
		public string ShortNote { get; set; }

		[Display(Name = "Order position")]
		public int OderPosition { get; set; }

		[Display(Name = "Activate category")]
		public bool IsActive { get; set; }

		[Display(Name = "Created date")]
		public DateTime CreatedAt { get; set; }
		public bool Deleted { get; set; }

	}
}
