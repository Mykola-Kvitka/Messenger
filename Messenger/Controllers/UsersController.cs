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
    public class UsersController : Controller
    {
        UserManager<UserEntity> _userManager;

        public UsersController(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index(int page = 1)
        {

            var result = new AdminUsersViewModel();

            if (page == 1)
            {
                result.TotalCount = _userManager.Users.Count();
            }


            result.Users = _userManager.Users.Skip((page - 1) * 15).ToList();

            result.Page = page;

            return View(result);
        }
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserEntity user = new UserEntity { Email = model.Email, UserName = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
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
            return View(model);
        }

        [HttpGet("users/edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            UserEntity user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost("users/edit/{id}")]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserEntity user = await _userManager.FindByIdAsync(model.Id.ToString());
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;

                    var result = await _userManager.UpdateAsync(user);
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
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            UserEntity user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChangePassword(Guid id)
        {
            UserEntity user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserEntity user = await _userManager.FindByIdAsync(model.Id.ToString());
                if (user != null)
                {
                    var _passwordValidator =
                    HttpContext.RequestServices.GetService(typeof(IPasswordValidator<UserEntity>)) as IPasswordValidator<UserEntity>;
                    var _passwordHasher =
                    HttpContext.RequestServices.GetService(typeof(IPasswordHasher<UserEntity>)) as IPasswordHasher<UserEntity>;

                    IdentityResult result =
                    await _passwordValidator.ValidateAsync(_userManager, user, model.NewPassword);
                    if (result.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                        await _userManager.UpdateAsync(user);
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
                else
                {
                    ModelState.AddModelError(string.Empty, "Користувач не знайдений");
                }
            }
            return View(model);
        }
    }
}
