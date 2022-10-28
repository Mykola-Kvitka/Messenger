using AutoMapper;
using Messenger.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messenger.DAL.Models;
using Messenger.BLL.Models;
using Messenger.PL.ViewModels;

namespace Messenger.PL.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IChatServise _chatService;
        private readonly IContactService _contactService;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;

        public ChatController(IChatServise chatService, IMapper mapper, UserManager<UserEntity> userManager,IContactService contactService)
        { 
            _userManager = userManager;
            _chatService = chatService;
            _contactService = contactService;
            _mapper = mapper;
        }

        [HttpGet("chat")]
        public async Task<ActionResult> Index()
        {
            var user = (await _userManager.GetUserAsync(HttpContext.User)).Id;

            var chatVm = _mapper.Map<IEnumerable<DisplayChats>, IEnumerable<DisplayChatsViewModel>>(await _chatService.GetAllAsync(user));

            return View(chatVm);
        }

        [HttpGet("chat/create")]
        public async Task<ActionResult> Create()
        {
            var user = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            var contacts = _mapper.Map<List<Contact>,List<ContactViewModel>>((await _contactService.FindAsync(user)).ToList());

            ViewBag.Users = new SelectList(contacts, "UserId", "UserName");

            return View();
        }

        [HttpPost("chat/create")]
        public async Task<ActionResult> CreateAsync(CreateChatViewModel requestVm)
        {
            var user = (await _userManager.GetUserAsync(HttpContext.User));
            requestVm.User.Add(user);

             await _chatService.CreateAsync(_mapper.Map<CreateChatViewModel,CreateChat>(requestVm));

            return Redirect("~/chat");
        }

        [HttpPost("chat/delete")]
        public async Task<ActionResult> Delete(DeleteChatViewModel requestVm)
        {
            var user = (await _userManager.GetUserAsync(HttpContext.User));
            requestVm.UserId = user.Id;

             _chatService.DeleteAsync(_mapper.Map<DeleteChatViewModel, DeleteChat>(requestVm));

            return Redirect("~/chat");
        }
    }
}
