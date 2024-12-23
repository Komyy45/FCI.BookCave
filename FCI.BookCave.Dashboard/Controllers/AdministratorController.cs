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
        private RoleManager<IdentityRole> _roleManager;


        public AdministratorController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        #region User

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
        public async Task<IActionResult> Register(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                var newuser = new ApplicationUser
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.phoneNumber,
                    EmailConfirmed = true,
                    DisplayName = user.UserName.ToUpper(),
                };

                var result = await _userManager.CreateAsync(newuser, user.Password);

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

        public async Task<ActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
                return RedirectToAction("GetAllUsers");
            await _userManager.DeleteAsync(user);
            return View("UsersList");
        }
        #endregion

        #region Role
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleManager.Roles.Select(r => new RoleViewModel
            // de tareqa tanya brdo ashl
            {
                Id = r.Id,
                RoleName = r.Name
            }).ToListAsync();
            // de tareqet l mapping wa tb3n fe l return view htrg3 l rolelist
            //List<RoleViewModel> rolelist = new List<RoleViewModel>();
            //         foreach (var role in roles)
            //         {
            //	RoleViewModel roleviewmodel = new RoleViewModel { Id = role.Id, RoleName = role.Name };
            //	rolelist.Add(roleviewmodel);
            //         }

            return View("Roles", roles);
        }
        public async Task<IActionResult> CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel item)
        {
            if (ModelState.IsValid)
            {
                var newrole = new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = item.RoleName
                };
                var result = await _roleManager.CreateAsync(newrole);
                if (result.Succeeded)
                {
                    return RedirectToAction("GetAllRoles");
                }
                foreach (var errors in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, errors.Description);
                }
            }
            return View(item);
        }
        #endregion

    }
}
