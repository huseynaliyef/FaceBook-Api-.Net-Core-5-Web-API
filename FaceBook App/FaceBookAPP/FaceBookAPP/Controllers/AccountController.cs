using FaceBookAPP.Entities;
using FaceBookAPP.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FaceBookAPP.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IWebHostEnvironment _env;
        public AccountController(UserManager<IdentityUser> userManager,
                                RoleManager<IdentityRole> roleManager,
                                SignInManager<IdentityUser> signInManager,
                                IWebHostEnvironment env)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _env = env;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromForm]UserRegisterUIDTO u)
        {
            if (ModelState.IsValid)
            {
                Istifadeci istifadeci = new Istifadeci();
                istifadeci.UserName = u.UserName;
                istifadeci.Email = u.Email;

                var photoName = string.Concat(Guid.NewGuid().ToString(), u.UserPhoto.FileName);
                var folderpath = Path.Combine(_env.WebRootPath, "Photos");
                var filepath = Path.Combine(folderpath, photoName);
                if (!Directory.Exists(folderpath))
                {
                    Directory.CreateDirectory(folderpath);
                }
                
                using(FileStream fs = new FileStream(filepath, FileMode.Create))
                {
                    u.UserPhoto.CopyTo(fs);
                }
                istifadeci.ProfilPhoto = photoName;
                var result = await _userManager.CreateAsync(istifadeci,u.Password);
                if(result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(istifadeci, "User");
                    return StatusCode(StatusCodes.Status201Created, istifadeci.UserName);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Model is not valid");
            }
            
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleCreateUIDTO r)
        {
            IdentityRole role = new IdentityRole();
            role.Name = r.RoleName;
            await _roleManager.CreateAsync(role);
            return StatusCode(StatusCodes.Status201Created, r.RoleName);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginUIDTO u)
        {
            var user = await _userManager.FindByEmailAsync(u.Email);
            if(user != null)
            {
                var checkPass = await _userManager.CheckPasswordAsync(user,u.Password);
                if (checkPass)
                {
                    await _signInManager.PasswordSignInAsync(user, u.Password, true, false);
                    return Ok(user.Email);
                }
                else
                {
                    return BadRequest("Incorrect Email or Password!");
                }
            }
            else
            {
                return NotFound("User Not Found!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Successfully Sigout");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromForm]UserUpdateUIDTO u)
        {
            Istifadeci user = (Istifadeci)await _userManager.FindByEmailAsync(u.Email);
            if(user != null)
            {
                var checkPass = await _userManager.CheckPasswordAsync(user, u.oldPassword);
                if (checkPass)
                {
                    user.UserName = u.UserName;
                    await _userManager.ChangePasswordAsync(user, u.oldPassword, u.newPassword);
                    if(u.ProfilPhoto != null)
                    {
                        var photoName = string.Concat(Guid.NewGuid().ToString(), u.ProfilPhoto.FileName);
                        var folderpath = Path.Combine(_env.WebRootPath, "Photos");
                        var filepath = Path.Combine(folderpath, photoName);

                        using (FileStream fs = new FileStream(filepath, FileMode.Create))
                        {
                            u.ProfilPhoto.CopyTo(fs);
                        }
                        user.ProfilPhoto = photoName;
                        await _userManager.UpdateAsync(user);
                    }
                    return Ok("Successfully Update.");
                }
                else
                {
                    return BadRequest("Incorrect Email or Password!");
                }
            }
            else
            {
                return NotFound("User Not Found!");
            }
        }

        
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteProfilePhoto()
        {
            Istifadeci user = (Istifadeci)await _userManager.GetUserAsync(User);
            if (user != null)
            {
                user.ProfilPhoto = null;
                await _userManager.UpdateAsync(user);
            }
            return Ok(user.UserName + ". Your profile photo is deleted succefully.");
        }
    }
}
