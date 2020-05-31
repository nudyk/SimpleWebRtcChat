using SimpleWebRtcChat.Web.Entity.Entityes;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleWebRtcChat.Web.Entity.Repository
{
	public interface IRepository<T> where T : BaseEntity
	{
		IQueryable<T> Get();

		IQueryable<T> Get(Expression<Func<T, bool>> predicate);

		void Save(T entity);

		void Delete(T entity);
	}
}