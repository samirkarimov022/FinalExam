using FinalExam.Areas.Admin.ViewModels;
using FinalExam.Models;
using FinalExam.Utilities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using NuGet.Protocol.Plugins;

namespace FinalExam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            AppUser user = new AppUser
            {
                Name = register.Name,
                Email = register.Email,
                Surname = register.Surname,
                UserName = register.Username
            };
            IdentityResult result=await _userManager.CreateAsync(user,register.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                    return View(register);
                }
            }
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            AppUser user = await _userManager.FindByNameAsync(login.UsernameOrEmail);
            if(user == null)
            {
                user = await _userManager.FindByEmailAsync(login.UsernameOrEmail);
                if (user == null)
                {
                    ModelState.AddModelError(String.Empty, "Username,Email or Password incorrect");
                    return View(login);
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.IsRemember, true);
            if(result.IsLockedOut)
            {
                ModelState.AddModelError(String.Empty, "Login is not enable please try later");
                return View(login);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError(String.Empty, "Username, Email or Password incorrect");
                return View(login);
            }

            return RedirectToAction("Index", "Home", new { Area = "" });
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
        public async Task<IActionResult> CreateRoles()
        {
            foreach (UserRole role in Enum.GetValues(typeof(UserRole)))
            {
                if(!(await _roleManager.RoleExistsAsync(role.ToString())))
                {
                    await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString(),
                    });
                }
            }
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
    }
}
