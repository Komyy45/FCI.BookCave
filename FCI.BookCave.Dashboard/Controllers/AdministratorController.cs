using FCI.BookCave.Abstractions.Contracts;
using FCI.BookCave.Abstractions.Models.Identity;
using FCI.BookCave.Dashboard.Models.Adminstration;
using FCI.BookCave.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FCI.BookCave.Dashboard.Controllers
{
    public class AdministratorController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AdministratorController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> GetAllUsers()
        {
            var user = await _userManager.Users.Select(u => new UserViewModel()
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email
            }).ToListAsync();
            return View("UsersList", user);
        }


        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newuser = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(newuser, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newuser, "UsersRole");
                    await _signInManager.SignInAsync(newuser, isPersistent: false);
                    return RedirectToAction("GetAllUsers", "Administrator");
                }
                else
                {
                    foreach (var errors in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, errors.Description);
                    }
                }
            }
            return RedirectToAction("GetAllUsers", "Administrator");
        }
    }
}
