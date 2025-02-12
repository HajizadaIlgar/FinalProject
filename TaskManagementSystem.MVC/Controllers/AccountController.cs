using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TaskManagementSystem.BL.DTOs.AccountDTOs;
using TaskManagementSystem.BL.Extensions;
using TaskManagementSystem.BL.Services.Abstracts;
using TaskManagementSystem.CORE.Entities.Users;
using TaskManagementSystem.CORE.Enums;
using TaskManagementSystem.MVC.SignalR.Hubs;

namespace TaskManagementSystem.MVC.Controllers
{
    public class AccountController(IWebHostEnvironment _env, UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, RoleManager<IdentityRole> _roleManager, IHubContext<ChatHub> _hubContext, ICategoryService _service) : Controller
    {
        public bool IsAuthenticated => HttpContext?.User.Identity.IsAuthenticated ?? false;
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid) return View();
            string newFilePath = Path.Combine(_env.WebRootPath, "account");
            AppUser user = new AppUser
            {
                FullName = dto.UserName,
                UserName = dto.UserName,
                Email = dto.Email,
                ProfileImagerlUrl = newFilePath
            };
            if (dto.ProfileImage is not null)
            {
                user.ProfileImagerlUrl = await dto.ProfileImage.UploadAsync(newFilePath);
            }
            var userCreate = await _userManager.CreateAsync(user, dto.Password);

            if (!userCreate.Succeeded)
            {
                foreach (var err in userCreate.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View(dto);
            }
            var roleresult = await _userManager.AddToRoleAsync(user, Roles.User.AddRole());
            if (!roleresult.Succeeded)
            {
                foreach (var err in roleresult.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View(dto);
            }

            return RedirectToAction("Login", "Account");
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            //if (!IsAuthenticated) return RedirectToAction("Register", "Account");
            if (!ModelState.IsValid) return View();
            AppUser user = null;
            if (dto.UserNameOrEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(dto.UserNameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(dto.UserNameOrEmail);
            }
            if (user is null)
            {
                ModelState.AddModelError("", "Bele bir istifadeci tapilmadi !!!");
            }
            var signInProccess = await _signInManager.PasswordSignInAsync(user, dto.Password, true, false);
            await _hubContext.Clients.All.SendAsync("UserOnline", user.Id, user.UserName);
            if (!signInProccess.Succeeded)
            {
                if (signInProccess.IsLockedOut)
                {
                    ModelState.AddModelError("", "Sen 1 deqiqelik bloklandin gede");
                }
                if (signInProccess.IsNotAllowed)
                {
                    ModelState.AddModelError("", "username ve ya password sehvdi");
                }
                return View(dto);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> MyRoleMethod()
        {
            foreach (Roles item in Enum.GetValues(typeof(Roles)))
            {
                var roleName = item.AddRole();
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    var createRoleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                    if (!createRoleResult.Succeeded)
                    {
                        return BadRequest($"Rol '{roleName}' yaradılmadı.");
                    }
                }
            }
            return Ok();
        }

        [Route("api/user/current")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _service.GetCurrentUserAsync();
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new AppUser
            {
                UserName = user.UserName,
                ProfileImagerlUrl = user.ProfileImagerlUrl
            });
        }

    }
}
