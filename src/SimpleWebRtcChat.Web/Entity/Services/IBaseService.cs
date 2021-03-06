﻿using SimpleWebRtcChat.Web.Entity.Entityes;
using System;
using System.Collections.Generic;

namespace SimpleWebRtcChat.Web.Entity.Services
{
	public interface IBaseService<T>
		where T : BaseEntity
	{
		T Get(int key);

		IEnumerable<T> GetAll();

		IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> predicate);

		void Save(T entity, bool isCommit);
	}
}