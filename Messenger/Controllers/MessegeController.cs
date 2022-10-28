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
    public class MessegeController : Controller
    {
        private readonly IMessegeService _messageService;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;

        public MessegeController(IMessegeService messageService, IMapper mapper, UserManager<UserEntity> userManager)
        { 
            _userManager = userManager;
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet("messege/{id}")]
        public async Task<ActionResult> Index(Guid id, int page = 1)
        {
            var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;

            var chatMassage =  await _messageService.GetPagedAsync(id,userId, page);

            var massagePage = new MessegePagedViewModel() { 
                Massages = _mapper.Map<IEnumerable<Messege>, 
                IEnumerable<MessegeViewModel>>(chatMassage).OrderBy(a => a.CreateDate),
                Page = page,
                ChatId = id,
                TotalCount = await _messageService.GetCountAsync() };
            
            return View(massagePage);
        }

        [HttpPost("messege/create")]
        public async Task<ActionResult> Create(MessegeViewModel requestVm)
        {
            var user = (await _userManager.GetUserAsync(HttpContext.User));

            requestVm.OwnerId = user.Id;
            requestVm.UserName = user.UserName;

            await _messageService.CreateAsync(_mapper.Map<MessegeViewModel, Messege>(requestVm));

            return Redirect("~/messege/" + requestVm.ChatId);
        }      
        
        [HttpPost("messege/replay")]
        public async Task<ActionResult> ReplayAsync(MessegeViewModel requestVm)
        {
            var user = (await _userManager.GetUserAsync(HttpContext.User));

            requestVm.Massage = "@" + requestVm.UserName + " " + requestVm.Massage;

            requestVm.OwnerId = user.Id;
            requestVm.UserName = user.UserName;

            await _messageService.CreateAsync(_mapper.Map<MessegeViewModel, Messege>(requestVm));

            return Redirect("~/messege/" + requestVm.ChatId);
        }

        [HttpPost("messege/delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var red = (await _messageService.GetAsync(id)).ChatId;
            await _messageService.DeleteAsync(id);

            return Redirect("~/messege/" + red);
        }

        [HttpPost("messege/softdelete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            var red = (await _messageService.GetAsync(id)).ChatId;

            await _messageService.SoftDeleteAsync(id);

            return Redirect("~/messege/" + red);
        }

    }

}

