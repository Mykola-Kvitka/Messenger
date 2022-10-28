using Messenger.DAL.Models;
using Messenger.PL.ViewModels.Account;
using Messenger.PL.ViewModels.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.PL.Controllers
{
    public class RolesController : Controller
    {
        RoleManager<RoleEntity> _roleManager;
        UserManager<UserEntity> _userManager;

        public RolesController(RoleManager<RoleEntity> roleManager, UserManager<UserEntity> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index(int page = 1) {

            var result = new AdminRolesViewModel();

            if (page == 1)
            {
                result.TotalCount = _roleManager.Roles.Count();
            }


            result.Roles  = _roleManager.Roles.Skip((page - 1) * 15).ToList();

            result.Page = page;

            return View(result);
       }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new RoleEntity { Name = name });
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            RoleEntity role = await _roleManager.FindByNameAsync(id.ToString());
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }

        public IActionResult UserList() => View(_userManager.Users.ToList());

        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            // получаем пользователя
            UserEntity user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid userId, List<string> roles)
        {
            // получаем пользователя
            UserEntity user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                // получаем все роли
                var allRoles = _roleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return Redirect("~/Users/Index");
            }

            return NotFound();
        }
    }
}
