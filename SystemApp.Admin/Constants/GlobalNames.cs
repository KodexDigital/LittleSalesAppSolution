using System;
using System.Collections.Generic;
using System.Text;

namespace SystemApp.Admin.Constants
{
	public static class GlobalNames
	{
		public const string ProductNamePreffix = "SP_"; // subject to change based on the domain name
		public static readonly Guid GeneralID = Guid.NewGuid();
		public const string AlphaNumeric = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
		public const string Numeric = "0123456789";
		public static readonly Random random = new Random();
		public const string Seperaotor = "_";
		public static string DateFormat = DateTime.Now.ToString("MM/dd/yy hh:mm tt");
		public static readonly string ProductImagePath = "images/product/uploads";
	}
}
