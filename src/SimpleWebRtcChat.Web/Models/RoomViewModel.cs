using System.Collections.Generic;
using System.ComponentModel;

namespace SimpleWebRtcChat.Web.Models
{
	public class RoomViewModel
	{
		[DisplayName("Room Name")]
		public string Name { get; set; }

		public string Uid { get; set; }
		public UserViewModel User { get; set; }
		public List<UserViewModel> Users { get; set; }
		public int CreatorUserId { get; set; }
	}
}