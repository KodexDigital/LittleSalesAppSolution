using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SystemApp.Models.DataModels;

namespace SystemApp.Models.ViewModels
{
	public class RegisterViewModel
	{
		public RegisterViewModel()
		{

		}

		public RegisterViewModel(ApplicationUser user)
		{
			user.Name = Name;
			user.PhoneNumber = PhoneNumber;
			user.PasswordHash = PasswordHash;
			user.Email = Email;
			user.UserName = UserName;
			user.State = State;
			user.City = City;
			user.StreetAddress = StreetAddress;
			user.PostalCode = PostalCode;
			user.UserImagePath = UserImagePath;
		}

		[PersonalData]
		[Key]
		public virtual Guid Id { get; set; }

		[Required(ErrorMessage = "Enter your phone number")]
		[ProtectedPersonalData]
		[Display(Name = "Phone number")]
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }

		[Required(ErrorMessage = "Enter your password")]
		[Display(Name = "Password")]
		[DataType(DataType.Password)]
		public string PasswordHash { get; set; }

		[Required(ErrorMessage = "Retype your password")]
		[Display(Name = "Confirm password")]
		[DataType(DataType.Password)]
		[Compare(nameof(PasswordHash), ErrorMessage = "Password not matched")]
		public string ConfirmPassword { get; set; }

		[ProtectedPersonalData]
		[Required(ErrorMessage = "Enter your email address")]
		[Display(Name = "Email address")]
		[EmailAddress(ErrorMessage = "Invalid email address format")]
		public string Email { get; set; }

		[ProtectedPersonalData]
		[Required(ErrorMessage = "Enter your username")]
		[Display(Name = "Username")]
		public string UserName { get; set; }

		[ProtectedPersonalData]
		[Required(ErrorMessage = "Enter your name")]
		public string Name { get; set; }

		[Display(Name = "Street address")]
		public string StreetAddress { get; set; }
		public string City { get; set; }
		public string State { get; set; }

		[Display(Name = "Postal code")]
		[DataType(DataType.PostalCode)]
		public string PostalCode { get; set; }

		[ProtectedPersonalData]
		[Required(ErrorMessage = "Select an image")]
		[Display(Name = "Profile image")]
		[DataType(DataType.ImageUrl)]
		public IFormFile ProfileImageFilePath { get; set; }
		public string UserImagePath { get; set; }
	}
}
