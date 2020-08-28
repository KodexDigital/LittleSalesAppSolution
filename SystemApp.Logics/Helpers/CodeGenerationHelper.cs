using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemApp.Admin.Constants;

namespace SystemApp.Logics.Helpers
{
	public static class CodeGenerationHelper
	{
		static string namePreffix = GlobalNames.ProductNamePreffix;
		static string id = GlobalNames.GeneralID.ToString().Substring(0,3);
		static string characters = GlobalNames.AlphaNumeric;
		static string numberCharacters = GlobalNames.Numeric;
		public static string ProductNumber()
		{
			string productNumber = $"{namePreffix}{id}{GlobalNames.Seperaotor}{RandomString(4)}";
			return productNumber;
		}

		public static string ProductCode()
		{
			string productCode = $"{namePreffix}{id}{GlobalNames.Seperaotor}{RandomNumber(8)}";
			return productCode;
		}

		public static string RandomString(int length)
		{
			return new string(Enumerable.Repeat(characters, length)
			  .Select(s => s[GlobalNames.random.Next(s.Length)]).ToArray());
		}

		public static string RandomNumber(int lenght)
		{
			return new string(Enumerable.Repeat(numberCharacters, lenght).Select(n => n[GlobalNames.random.Next(n.Length)]).ToArray());
		}
	}
}
