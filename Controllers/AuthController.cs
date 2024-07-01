using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeeklyProgram.Dtos;
using WeeklyProgram.Models;

namespace WeeklyProgram.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _sigInManager;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> sigInManager)
        {
            _userManager = userManager;
            _sigInManager = sigInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userLoginDto.Email!);
                if (user is not null)
                {
                    var result = await _sigInManager.PasswordSignInAsync(user, userLoginDto.Password!, userLoginDto.RememberMe, false);
                    if (result.Succeeded)
                    {
                        
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "يوجد خطا في البريد الالكتروني او كلمة السر");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "يوجد خطا في البريد الالكتروني او كلمة السر");
                }
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _sigInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}