using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System.Repository.Admin
{
	public interface IDefaultRepository<TModel> where TModel : class
	{
		TModel Get(int id);
		TModel Get(string id);
		TModel Get(Guid id);
		void Remove(int id);
		void Remove(string id);
		void Remove(Guid id);
		void Remove(TModel entity);
		Task<IEnumerable<TModel>> GetAll(Expression<Func<TModel, bool>> filter = null, 
			Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, 
			string IncludeProperty = null);
		TModel GetFirstOrDefault(
		   Expression<Func<TModel, bool>> filter = null,
		   string IncludeProperty = null
		   );

		Task Add(TModel entity);
	}
}
