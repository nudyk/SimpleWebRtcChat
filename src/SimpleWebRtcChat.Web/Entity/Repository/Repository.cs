using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimpleWebRtcChat.Web.Entity.Entityes;

namespace SimpleWebRtcChat.Web.Entity.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Save(T entity)
        {
            var asLogedEntity = entity as BaseLoggedEntity;
            if (entity.Id > 0)
            {
                
                if(asLogedEntity != null)
                {
                    asLogedEntity.UpdatedDate = DateTime.UtcNow;
                }
                _unitOfWork.Context.Entry(entity).State = EntityState.Modified;
                _unitOfWork.Context.Set<T>().Attach(entity);
                _unitOfWork.Context.Set<T>().Update(entity);
            }
            else
            {
                if (asLogedEntity != null)
                {
                    asLogedEntity.CreatedDate = DateTime.UtcNow;
                }
                _unitOfWork.Context.Set<T>().Add(entity);
            }
        }

        public void Delete(T entity)
        {
            T existing = _unitOfWork.Context.Set<T>().Find(entity);
            if (existing != null) _unitOfWork.Context.Set<T>().Remove(existing);
        }

        public IQueryable<T> Get()
        {
            return _unitOfWork.Context.Set<T>();
        }

        public IQueryable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _unitOfWork.Context.Set<T>().Where(predicate);
        }
    }
}
