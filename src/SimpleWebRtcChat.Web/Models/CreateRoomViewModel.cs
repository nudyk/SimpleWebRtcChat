using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebRtcChat.Web.Models
{
    public class CreateRoomViewModel
    {
        public string Uid { get; set; }
        [Required]
        [DisplayName("Room Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("User Name")]
        public string UserName { get; set; }
    }
}
