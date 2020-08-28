using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System.Repository.Admin
{
	public class DefaultRepository<TModel> : IDefaultRepository<TModel> where TModel : class
	{
		protected readonly DbContext context;
		protected internal DbSet<TModel> dbSet;
		public DefaultRepository(DbContext context)
		{
			this.context = context;
			dbSet = context.Set<TModel>();
		}
		public async Task Add(TModel entity)
		{
			await dbSet.AddAsync(entity);
		}

		public TModel Get(int id)
		{
			return dbSet.Find(id);
		}

		public TModel Get(string id)
		{
			return dbSet.Find(id);
		}

		public TModel Get(Guid id)
		{
			return dbSet.Find(id);
		}

		public async Task<IEnumerable<TModel>> GetAll(Expression<Func<TModel, bool>> filter = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, string IncludeProperty = null)
		{
			IQueryable<TModel> query = dbSet;
			if (filter != null)
				query = query.Where(filter);
			if (orderBy != null)
				return orderBy(query).ToList();
			if (IncludeProperty != null)
				foreach (var includeProperties in IncludeProperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
					query = query.Include(includeProperties);
			return await query.ToListAsync();
		}

		public TModel GetFirstOrDefault(Expression<Func<TModel, bool>> filter = null, string IncludeProperty = null)
		{
			IQueryable<TModel> query = dbSet;
			if (filter != null)
				query = query.Where(filter);
			if (IncludeProperty != null)
				foreach (var includeProperties in IncludeProperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
					query = query.Include(includeProperties);
			return query.FirstOrDefault();
		}

		public void Remove(int id)
		{
			TModel obj = dbSet.Find(id);
			Remove(obj);
		}

		public void Remove(string id)
		{
			TModel obj = dbSet.Find(id);
			Remove(obj);
		}

		public void Remove(Guid id)
		{
			TModel obj = dbSet.Find(id);
			Remove(obj);
		}

		public void Remove(TModel entity)
		{
			dbSet.Remove(entity);
		}
	}
}
