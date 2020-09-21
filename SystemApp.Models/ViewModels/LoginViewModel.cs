using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SystemApp.Models.DataModels;

namespace SystemApp.Models.ViewModels
{
	public class LoginViewModel
	{
		public LoginViewModel()
		{

		}
		public LoginViewModel(ApplicationUser login)
		{
			login.Email = Email;
			login.PasswordHash = Password;
		}

		[Required]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		[Display(Name = "E-mail address")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Stay signed in")]
		public bool RememberMe { get; set; }

		public string ReturnUrl { get; set; }

	}
}
