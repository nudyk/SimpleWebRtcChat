using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleWebRtcChat.Web.Entity.Entityes
{
	public class Room : BaseLoggedEntity
	{
		public string Name { get; set; }
		public string Uid { get; set; }

		public int CreatorId { get; set; }

		[ForeignKey("CreatorId")]
		public virtual User Creator { get; set; }

		//public virtual ICollection<User> Users { get; set; }
	}
}