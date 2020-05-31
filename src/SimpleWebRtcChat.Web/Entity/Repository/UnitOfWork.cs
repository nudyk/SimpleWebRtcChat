namespace SimpleWebRtcChat.Web.Entity.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		public DataDbContext Context { get; }

		public UnitOfWork(DataDbContext context)
		{
			Context = context;
		}

		public void Commit()
		{
			Context.SaveChanges();
		}

		public void Dispose()
		{
			Context.Dispose();
		}
	}
}