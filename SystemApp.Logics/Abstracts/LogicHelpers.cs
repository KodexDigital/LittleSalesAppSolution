using System;
using System.Collections.Generic;
using System.Text;

namespace SystemApp.Logics.Abstracts
{
	public abstract class LogicHelpers<T> where T : class
	{
		protected abstract double CalculateTotal();
	}
}
