using System;
using System.Collections.Generic;
using System.Text;
using SystemApp.Logics.Abstracts;

namespace SystemApp.Logics.Helpers
{
	public class FileUploadHelper<TModel> : FileUploader<TModel> where TModel : class
	{
		protected override string Upload(TModel model)
		{
			throw new NotImplementedException();
		}

	}
}
