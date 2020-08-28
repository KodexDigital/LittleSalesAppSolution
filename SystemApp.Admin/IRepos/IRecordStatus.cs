using System;
using System.Collections.Generic;
using System.Text;

namespace SystemApp.Admin.IRepos
{
	public partial interface IRecordStatus
	{
		/// <summary>
		/// Sets the current record status: Default is true
		/// </summary>
		bool IsActive { get; set; }
	}
}
