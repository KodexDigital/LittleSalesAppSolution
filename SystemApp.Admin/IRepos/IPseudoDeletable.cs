using System;
using System.Collections.Generic;
using System.Text;

namespace SystemApp.Admin.IRepos
{
	public partial interface IPseudoDeletable
	{
		/// <summary>
		/// If applied the record will be in delete mode:
		/// </summary>
		bool Delete { get; set; }
	}
}
