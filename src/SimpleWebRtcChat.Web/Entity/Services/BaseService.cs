using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleWebRtcChat.Web.Entity.Entityes;
using SimpleWebRtcChat.Web.Entity.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleWebRtcChat.Web.Entity.Services
{
	public abstract class BaseService<T> : IBaseService<T>
		where T : BaseEntity
	{
		protected readonly IUnitOfWork _unitOfWork;
		protected readonly IRepository<T> _typeRepository;

		public BaseService(IUnitOfWork unitOfWork, IRepository<T> typeRepository)
		{
			_unitOfWork = unitOfWork;
			_typeRepository = typeRepository;
		}

		public T Get(int key)
		{
			var result = _typeRepository.Get(p => p.Id == key).FirstOrDefault();
			return result;
		}

		public IEnumerable<T> GetAll()
		{
			var result = _typeRepository.Get();
			return result;
		}

		public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
		{
			var result = _typeRepository.Get(predicate);
			return result;
		}

		public void Save(T entity, bool isCommit)
		{
			if (entity.Id > 0)
			{
				var existEntity = Get(entity.Id);
				Update(existEntity, entity);
			}

			DisplayStates(_unitOfWork.Context.ChangeTracker.Entries());
			_typeRepository.Save(entity);
			if (isCommit)
			{
				_unitOfWork.Commit();
			}
		}

		public abstract void Update(T dbEntity, T updatedEntity);

		private static void DisplayStates(IEnumerable<EntityEntry> entries)
		{
			foreach (var entry in entries)
			{
				Debug.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: { entry.State.ToString()}");
			}
		}
	}
}