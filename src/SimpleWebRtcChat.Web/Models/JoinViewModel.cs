using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebRtcChat.Web.Models
{
	public class JoinViewModel
	{
		[Required]
		public string RoomUid { get; set; }

		[Required]
		[DisplayName("User Name")]
		public string UserName { get; set; }

		[DisplayName("Room Name")]
		public string RoomName { get; set; }
	}
}