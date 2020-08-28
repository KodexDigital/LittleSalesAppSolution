using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SystemApp.Models.DataModels;

namespace SystemApp.Models.ViewModels
{
	public class ProductViewModel
	{
		public ProductViewModel()
		{}
		public ProductViewModel(Product product)
		{
			product.ProductName = ProductName;
			product.LowerCaseName = LowerCaseName;
			product.ProductNumber = ProductNumber;
			product.ProductCode = ProductCode;
			product.Category = Category;
			product.Description = Description;
			product.Price = Price;
			product.ProductImagePath = ProductImagePath;
		}

		public Guid Id { get; set; }

		[Required(ErrorMessage ="Enter product name")]
		[Display(Name ="Product name")]
		public string ProductName { get; set; }

		[Display(Name = "Normalized name")]
		public string LowerCaseName { get; set; }

		[Display(Name = "Product number")]
		public string ProductNumber { get; set; }

		[Display(Name = "Product code")]
		public string ProductCode { get; set; }

		[Required(ErrorMessage = "Select product category")]
		[Display(Name = "Product category")]
		public string Category { get; set; }

		[Required(ErrorMessage = "Enter product description")]
		[Display(Name = "Product description")]
		[StringLength(300)]
		public string Description { get; set; }

		[Required(ErrorMessage = "Enter price")]
		[Display(Name = "Price")]
		public double Price { get; set; }
		public Guid SalesOrder { get; set; }

		[Display(Name = "Deleted?")]
		public bool Delete { get; set; }

		[Display(Name = "Status")]
		public bool IsActive { get; set; }
		public string ProductImagePath { get; set; }

		[Required(ErrorMessage = "Please choose a product image")]
		public IFormFile FilePath { get; set; }

		[Display(Name = "Created date")]
		public DateTime CreatedAt { get; set; }

	}
}
