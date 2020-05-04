using AutoMapper;
using SimpleWebRtcChat.Web.Entity.Entityes;
using SimpleWebRtcChat.Web.Models;

namespace SimpleWebRtcChat.Web.Utils
{
    public class MyProfileMap : Profile
    {
        public MyProfileMap()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}