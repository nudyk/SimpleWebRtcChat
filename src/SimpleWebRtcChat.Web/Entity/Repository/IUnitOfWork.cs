using System;

namespace SimpleWebRtcChat.Web.Entity.Repository
{
	public interface IUnitOfWork : IDisposable
	{
		DataDbContext Context { get; }

		void Commit();
	}
}