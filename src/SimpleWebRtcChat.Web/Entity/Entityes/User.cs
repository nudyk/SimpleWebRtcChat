using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleWebRtcChat.Web.Entity.Entityes
{
	public class User : BaseEntity
	{
		public string Name { get; set; }

		[ForeignKey("Room")]
		public int RoomId { get; set; }

		public string PeerId { get; set; }
		public string ConnectionId { get; set; }
		public virtual Room Room { get; set; }
	}
}