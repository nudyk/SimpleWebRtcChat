using System.ComponentModel;

namespace SimpleWebRtcChat.Web.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [DisplayName("User Name")]
        public string Name { get; set; }
        public string PeerId { get; set; }
        public string ConnectionId {get; set; }
    }
}
