using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeeklyProgram.Dtos;
using WeeklyProgram.Models;

namespace WeeklyProgram.Controllers
{

    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly RoleManager<AppRole> _rolemanager;
        private readonly IMapper _mapper;


        public UserController(UserManager<AppUser> usermanager, IMapper mapper, RoleManager<AppRole> rolemanager)
        {
            _usermanager = usermanager;
            _mapper = mapper;
            _rolemanager = rolemanager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _usermanager.Users.ToListAsync();
            var map = _mapper.Map<List<UserDto>>(users);

            foreach (var item in map)
            {
                var finduser = await _usermanager.FindByIdAsync(item.Id.ToString());
                var role = string.Join("", await _usermanager.GetRolesAsync(finduser));
                item.Role = role;
            }
            return View(map);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var roles = await _rolemanager.Roles.ToListAsync();

            return View(new UserAddDto { Roles = roles });
        }
        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            var map = _mapper.Map<AppUser>(userAddDto);


            if (ModelState.IsValid)
            {
                map.UserName = userAddDto.Email;
                var result = await _usermanager.CreateAsync(map, userAddDto.Password!);
                if (result.Succeeded)
                {
                    var findRole = await _rolemanager.FindByIdAsync(userAddDto.RoleId.ToString());
                    await _usermanager.AddToRoleAsync(map, findRole!.ToString());
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                }
            }
            var roles = await _rolemanager.Roles.ToListAsync();
            userAddDto.Roles = roles;
            return View(userAddDto);
        }
    }
}