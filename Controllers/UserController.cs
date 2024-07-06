using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin,Superadmin")]
        public async Task<IActionResult> Index()
        {
            var users = await _usermanager.Users.ToListAsync();
            var map = _mapper.Map<List<UserDto>>(users);

            foreach (var item in map)
            {
                var finduser = await _usermanager.FindByIdAsync(item.Id.ToString());
                var role = string.Join("", await _usermanager.GetRolesAsync(finduser!));
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
        [ValidateAntiForgeryToken]
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
        [HttpGet]
        public async Task<IActionResult> Update(Guid UserId)
        {
            var user = await _usermanager.FindByIdAsync(UserId.ToString());
            var roles = await _rolemanager.Roles.ToListAsync();
            var map = _mapper.Map<UserUpdateDto>(user);
            var role = string.Join("", await _usermanager.GetRolesAsync(user!));
            var userRole = await _rolemanager.FindByNameAsync(role);
            map.RoleId = userRole.Id;
            map.Roles = roles;

            return View(map);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            var user = await _usermanager.FindByIdAsync(userUpdateDto.Id.ToString());
            if (user is not null)
            {
                var userRole = string.Join("", await _usermanager.GetRolesAsync(user));
                if (ModelState.IsValid)
                {
                    _mapper.Map(userUpdateDto, user);
                    user.UserName = userUpdateDto.Email;
                    user.SecurityStamp = Guid.NewGuid().ToString();
                    var result = await _usermanager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        var findRole = await _rolemanager.FindByIdAsync(userUpdateDto.RoleId.ToString());
                        await _usermanager.RemoveFromRoleAsync(user, userRole);
                        await _usermanager.AddToRoleAsync(user, findRole.Name);
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
            }
            userUpdateDto.Roles = await _rolemanager.Roles.ToListAsync();
            return View(userUpdateDto);
        }
        public async Task<IActionResult> Delete(Guid UserId)
        {
            var user = await _usermanager.FindByIdAsync(UserId.ToString());
            var result = await _usermanager.DeleteAsync(user!);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return NotFound();
        }
        
    }
}
