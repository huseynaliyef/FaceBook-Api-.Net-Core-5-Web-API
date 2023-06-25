using FaceBookAPP.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FaceBookAPP.DAL
{
    public class FaceBookDbContext:IdentityDbContext<IdentityUser>
    {
        public FaceBookDbContext(DbContextOptions<FaceBookDbContext> options):base(options)
        {}

        public DbSet<Istifadeci> Istifadecis { get; set; }
        
        public DbSet<UserPost> userPosts { get; set; }
    }
}
