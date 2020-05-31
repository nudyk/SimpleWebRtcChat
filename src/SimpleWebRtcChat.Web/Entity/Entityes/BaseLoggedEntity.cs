using System;

namespace SimpleWebRtcChat.Web.Entity.Entityes
{
	public class BaseLoggedEntity : BaseEntity
	{
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
	}
}