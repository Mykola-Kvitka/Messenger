using AutoMapper;
using Messenger.BLL.Interfaces;
using Messenger.BLL.Models;
using Messenger.DAL.Models;
using Messenger.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.PL.Controllers
{
    public class ContactController : Controller
    {
        private readonly IChatServise _chatService;
        private readonly IContactService _contactService;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;

        public ContactController(IChatServise chatService, IMapper mapper, UserManager<UserEntity> userManager, IContactService contactService)
        {
            _userManager = userManager;
            _chatService = chatService;
            _contactService = contactService;
            _mapper = mapper;
        }

        [HttpGet("contact")]
        public async Task<IActionResult> Index()
        {
            var user = (await _userManager.GetUserAsync(HttpContext.User)).Id;

            var chatVm = _mapper.Map<IEnumerable<Contact>, IEnumerable<ContactViewModel>>(await _contactService.GetPagedAsync(user));

            return View(chatVm);
        }

        [HttpGet("contact/error")]
        public IActionResult Error()
        {
            return View();
        }


        [HttpGet("contact/addcontact")]
        public  ActionResult Create()
        {
            return View();
        }

        [HttpPost("contact/addcontact")]
        public async Task<ActionResult> CreateAsync(ContactViewModel requestVm)
        {
            var request = await _userManager.GetUserAsync(HttpContext.User);
            //if (request.Email == requestVm.UserEmail)
            //{
            //    return Redirect("~/contact/error");
            //}
            
                requestVm.OwnerId = request.Id;

                var create = await _contactService.CreateAsync(_mapper.Map<ContactViewModel, Contact>(requestVm));

                if (create)
                {
                    return Redirect("~/contact");
                }
                else
                {
                    return Redirect("~/contact/error");
                }
            
        }

        [HttpGet("contact/delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            _contactService.DeleteAsync(id);
            return View();
        }

    }
}
