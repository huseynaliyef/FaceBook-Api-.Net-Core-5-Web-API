using FaceBookAPP.DAL;
using FaceBookAPP.Entities;
using FaceBookAPP.Models.DTO;
using FaceBookAPP.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace FaceBookAPP.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IGenericRepository<UserPost> _userPostTable;
        private readonly IWebHostEnvironment _env;
        private readonly IGenericPost _post;
        public UserController(UserManager<IdentityUser> userManager,
                              IWebHostEnvironment env,
                              IGenericRepository<UserPost> userPostTable,
                              IGenericPost post)
        {
            _userManager = userManager;
            _env = env;
            _userPostTable = userPostTable;
            _post = post;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SharePost([FromForm] UserPostUIDTO p)
        {
            UserPost post = new UserPost();
            
            var user = await _userManager.GetUserAsync(User);

            var photoName = string.Concat(Guid.NewGuid().ToString(), p.Photo.FileName);
            var folderPath = Path.Combine(_env.WebRootPath, "Posts");
            var filePath = Path.Combine(folderPath, photoName);
            if(!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using(FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                p.Photo.CopyTo(fs);
            }
            post.IstifadeciId = user.Id;
            post.Photo = photoName;
            post.Description = p.Description;
            await _userPostTable.AddAndCommit(post);
            return Ok(user.UserName + " Added new Post!");
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> EditPost([FromForm]PostEditUIDTO p)
        {
            Istifadeci i = (Istifadeci) await _userManager.GetUserAsync(User);
            var post = await _userPostTable.GetTableByExpration(m => m.IstifadeciId == i.Id && m.Id == p.PostId);
            if(post != null)
            {
                post.Description = p.Desription;
                if (p.Photo != null)
                {
                    var photoName = string.Concat(Guid.NewGuid().ToString(), p.Photo.FileName);
                    var folderPath = Path.Combine(_env.WebRootPath, "Posts");
                    var filePath = Path.Combine(folderPath, photoName);

                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        p.Photo.CopyTo(fs);
                    }
                    post.Photo = photoName;
                }
                await _userPostTable.Commit();
                return Ok("Post Successfully Updated.");
            }
            else
            {
                return NotFound("Wrong Operation");
            }
            
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetPostById(int id)
        {
            var Post = await _userPostTable.GetById(id);

            if (Post != null)
            {
                string Photo = await _post.GetPostBase64(id);
                return Ok(Photo);
            }
            else
            {
                return NotFound("Post not Found!");
            }
        }
    }
}
