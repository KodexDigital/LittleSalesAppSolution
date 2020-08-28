using System;
using System.Collections.Generic;
using System.Text;

namespace SystemApp.Logics.Abstracts
{
	public abstract class FileUploader<TModel> where TModel:class
	{
		protected abstract string Upload(TModel model);
	}
}
