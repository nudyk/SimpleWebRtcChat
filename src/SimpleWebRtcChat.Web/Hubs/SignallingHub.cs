using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using SimpleWebRtcChat.Web.Entity.Entityes;
using SimpleWebRtcChat.Web.Entity.Services;
using SimpleWebRtcChat.Web.Models;

namespace SimpleWebRtcChat.Web.Hubs
{
    public class SignallingHub : Hub
    {
        private readonly IUserService _userService;
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;

        private static class Commands
        {
            public static string OnUserJoin = "onUserJoin";
            public static string OnUserDisconected = "onUserDisconected";
        }
        public SignallingHub(IUserService userService, IRoomService roomService, IMapper mapper)
        {
            _userService = userService;
            _roomService = roomService;
            _mapper = mapper;
        }
        public async Task<List<UserViewModel>> UserJoin(string roomUid, string pearId, string userName)
        {
            var room = _roomService.GetAll(p => p.Uid == roomUid).First();
            var user = _userService.GetAll(p => p.RoomId == room.Id && p.Name == userName).First();
            user.PeerId = pearId;
            user.ConnectionId = Context.ConnectionId;
            _userService.Save(user, true);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomUid);
            await Clients.OthersInGroup(roomUid).SendAsync(Commands.OnUserJoin, pearId, userName);
            
            var existsUsers = _userService.GetAll(p => p.RoomId == room.Id && p.Name != userName);
            List<UserViewModel> result = _mapper.Map<List<UserViewModel>>(existsUsers);
            return result;
        }

        public override Task OnConnectedAsync()
        {
            Debug.WriteLine("COnnectionId: {0} Connected", Context.ConnectionId);
            return base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = _userService.GetAll(p => p.ConnectionId == Context.ConnectionId).FirstOrDefault();
            if (user != null)
            {
                var room = _roomService.Get(user.RoomId);
                if(room != null)
                {
                    await Clients.OthersInGroup(room.Uid).SendAsync(Commands.OnUserDisconected, user.PeerId, user.Name);
                }
            }
            Debug.WriteLine("ConnectionId: {0} Disconnected", Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

    }
}
