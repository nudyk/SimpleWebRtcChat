using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleWebRtcChat.Web.Entity.Entityes;
using SimpleWebRtcChat.Web.Entity.Services;
using SimpleWebRtcChat.Web.Models;

namespace SimpleWebRtcChat.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IRoomService _roomservice;
        private readonly IEncryptService _encryptService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IUserService userService, IRoomService roomservice, IEncryptService encryptService, IMapper mapper)
        {
            _logger = logger;
            _userService = userService;
            _roomservice = roomservice;
            _encryptService = encryptService;
            _mapper = mapper;
        }
         [HttpGet]
        public IActionResult Index()
        {
            var vm = new CreateRoomViewModel();
            return View(vm);
        }
        [HttpPost]
        public IActionResult Index(CreateRoomViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var user = new User {Name = vm.UserName};
            
            var room = new Room
            {
                Name = vm.Name,
                Uid = Guid.NewGuid().ToString().Replace("-", string.Empty),
                Creator = user,
                
            };
            user.Room = room;
            
            //_userService.Save(user, true);
            _roomservice.Save(room, true);
            string roomAndUserUid = WebUtility.UrlEncode(_encryptService.Encrypt($"{room.Uid}&{user.Name}"));
            return RedirectToAction("Room", new { roomAndUserUid = roomAndUserUid});
        }

        [HttpGet]
        public IActionResult Room(string roomAndUserUid)
        {
            var roomAndUserUidDecripted = WebUtility.UrlDecode(_encryptService.Decrypt(roomAndUserUid));
            var roomAndUser = roomAndUserUidDecripted.Split('&');
            var roomUid = roomAndUser[0];
            var userName = roomAndUser[1];
            var room = _roomservice.GetAll(p => p.Uid == roomUid).First();
            var user = _userService.GetAll(p => p.RoomId == room.Id && p.Name == userName).First();
            var userVm = _mapper.Map<UserViewModel>(user);
            var users = _userService.GetAll(p => p.Id != user.Id && p.RoomId == room.Id);
            var usersVm = _mapper.Map<List<UserViewModel>>(users);
            var vm = new RoomViewModel
            {
                Name = room.Name,
                Uid = roomUid,
                CreatorUserId = room.CreatorId,
                User = userVm,
                Users = usersVm
            };
            return View(vm);
        }
        [HttpGet]
        public IActionResult JoinRoom(string roomUid)
        {
            var vm = new JoinViewModel
            {
                RoomUid = roomUid
            };
            return View(vm);
        }
        
        [HttpPost]
        public IActionResult JoinRoom(JoinViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var room = _roomservice.GetAll(p => p.Uid == vm.RoomUid).First();
            if (_userService.GetAll(p => p.RoomId == room.Id && p.Name == vm.UserName).Any())
            {
                ModelState.AddModelError("UserName", "User already exists. Use another name");
                return View(vm);
            }
            var user = new User {Name = vm.UserName, RoomId = room.Id};
            _userService.Save(user, true);

            string roomAndUserUid = WebUtility.UrlEncode(_encryptService.Encrypt($"{room.Uid}&{user.Name}"));
            return RedirectToAction("Room", new { roomAndUserUid = roomAndUserUid});
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
