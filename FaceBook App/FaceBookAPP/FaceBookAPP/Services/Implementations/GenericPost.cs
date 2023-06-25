using FaceBookAPP.Entities;
using FaceBookAPP.Services.Abstractions;
using System.IO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace FaceBookAPP.Services.Implementations
{
    public class GenericPost : IGenericPost
    {
        private readonly IGenericRepository<UserPost> _userPostTable;
        private readonly IWebHostEnvironment _env;
        public GenericPost(IGenericRepository<UserPost> userPostTable,
                            IWebHostEnvironment env)
        {
            _userPostTable = userPostTable;
            _env = env;
        }
        public async Task<string> GetPostBase64(int Id)
        {
            var Post = await _userPostTable.GetById(Id);
            string Photo = null;
            if (Post != null)
            {
                var folderPath = Path.Combine(_env.WebRootPath, "Posts");
                var filePath = Path.Combine(folderPath, Post.Photo);
                byte[] data = default(byte[]);
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    data = new byte[fs.Length];
                    await fs.ReadAsync(data, 0, data.Length);
                }
                Photo = Convert.ToBase64String(data);
            }
            return Photo;
        }
    }
}
