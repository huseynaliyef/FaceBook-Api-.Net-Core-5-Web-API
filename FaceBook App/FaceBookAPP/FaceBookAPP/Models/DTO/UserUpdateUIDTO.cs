using Microsoft.AspNetCore.Http;

namespace FaceBookAPP.Models.DTO
{
    public class UserUpdateUIDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public IFormFile ProfilPhoto { get; set; }
    }
}
